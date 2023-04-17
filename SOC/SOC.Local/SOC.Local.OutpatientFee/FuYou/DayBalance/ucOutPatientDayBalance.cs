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

namespace FS.SOC.Local.OutpatientFee.FuYou.DayBalance
{
    /// <summary>
    /// 妇幼门诊发票日结
    /// {0F9CA67C-2A6A-413f-B1EE-8FC44CD1317A}
    /// </summary>
    public partial class ucOutPatientDayBalance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucOutPatientDayBalance()
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

        /// <summary>
        /// 日结方法类
        /// </summary>
        Function.OutPatientDayBalance clinicDayBalance = new Function.OutPatientDayBalance();
        // 门诊收费业务层
        FS.HISFC.BizLogic.Fee.Outpatient outpatient = new FS.HISFC.BizLogic.Fee.Outpatient();

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
        public List<Models.ClinicDayBalanceNew> alData = new List<Models.ClinicDayBalanceNew>();

        private Models.ClinicDayBalanceNew dayBalance = null;

        #region 新增

        /// <summary>
        /// 起始时间
        /// </summary>
        private DateTime beginDate = DateTime.MinValue;

        /// <summary>
        /// 终止时间
        /// </summary>
        private DateTime endDate = DateTime.MinValue;
      

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
                // {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
                if (empBalance == null)
                {
                    empBalance = new FS.HISFC.Models.Base.Employee();
                    empBalance.ID = "T00001"; 
                    empBalance.Name = "T-全院";
                }

                // 返回值
                int intReturn = 0;

                // 获取当前操作员
                currentOperator = this.clinicDayBalance.Operator;

                // 获取最近一次日结时间
                intReturn = this.GetLastBalanceDate();
                //本次日结开始时间为上次结束时间+1
                this.lastDate = NConvert.ToDateTime(this.lastDate).AddSeconds(1).ToString();
                if (intReturn == -1)
                {
                    MessageBox.Show("获取上次日结时间失败！不能进行日结操作！");
                    return;
                }
                else if (intReturn == 0)
                {
                    // 没有作过日结，设置上次日结时间为最小时间
                    this.lastDate = System.DateTime.MinValue.ToString();
                    this.ucClinicDayBalanceDateControl1.tbLastDate.Text = System.DateTime.MinValue.ToString();
                }
                else
                {
                    // 作过日结
                    this.ucClinicDayBalanceDateControl1.tbLastDate.Text = this.lastDate.ToString();
                    this.ucClinicDayBalanceDateControl1.dtpBalanceDate.Value = this.clinicDayBalance.GetDateTimeFromSysDateTime();
                }

                // 初始化子控件的变量
                this.ucClinicDayBalanceDateControl1.dtLastBalance = NConvert.ToDateTime(this.lastDate);
                this.ucClinicDayBalanceReportNew1.InitUC(clinicDayBalance.Hospital.Name + reportTitle);
                this.ucClinicDayBalanceReportNew2.InitUC(clinicDayBalance.Hospital.Name + reportTitle);
                this.ucClinicDayBalanceReportNew1.SetDetailName();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        #endregion

        #region 获取收款员上次日结时间

        /// <summary>
        /// 获取收款员上次日结时间
        /// </summary>
        /// <returns></returns>
        public int GetLastBalanceDate()
        {
            try
            {
                // 变量定义
                int intReturn = 0;

                // 获取收款员上次日结时间
                // {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
                if (this.isAll == "0")
                {
                    intReturn = outpatient.GetLastBalanceDate(this.empBalance, ref lastDate);
                }
                else
                {
                    intReturn = outpatient.GetLastBalanceDate(this.currentOperator, ref lastDate);
                }

                // 判断获取结果
                if (intReturn == -1)
                {
                    MessageBox.Show("获取收款员最近一次日结时间失败！");
                    return -1;
                }
                return intReturn;
            }
            catch
            {
                return -1;
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
            DataSet ds;
            int intReturn = 0;

            intReturn = this.ucClinicDayBalanceDateControl1.GetBalanceDate(ref dayBalanceDate);
            if (intReturn == -1)
            {
                return;
            }
            // 获取最近一次日结时间
            intReturn = this.GetLastBalanceDate();
            //本次日结开始时间为上次结束时间+1
            this.lastDate = NConvert.ToDateTime(this.lastDate).AddSeconds(1).ToString();
            if (intReturn == -1)
            {
                MessageBox.Show("获取上次日结时间失败！不能进行日结操作！");
                return;
            }
            else
            {
                this.ucClinicDayBalanceDateControl1.tbLastDate.Text = this.lastDate.ToString();
            }
            //显示报表信息
            this.ucClinicDayBalanceReportNew1.Clear(lastDate, dayBalanceDate);

            //获取日结算数据
            ds = new DataSet();

            // {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
            string strEmpID = this.currentOperator.ID;
            if (this.isAll == "0")
            {
                strEmpID = "ALL";
            }
            clinicDayBalance.GetDayBalanceDataMZRJ(strEmpID, this.lastDate, dayBalanceDate, ref ds);

            this.SetDetial(ds.Tables[0]);

            //设置farpoint格式
            this.ucClinicDayBalanceReportNew1.SetFarPoint();

            //显示发票汇总数据
            SetInvoice(this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1);

            //显示金额数据
            this.SetMoneyValue(this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1);
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
            if (ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Rows.Count > 0)
            {
                //先清空数据，然后初始化3行
                ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Rows.Count = 0;
                ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Rows.Count = 4;
                ucClinicDayBalanceReportNew1.SetDetailName();
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

                //decTemp += FS.FrameWork.Function.NConvert.ToDecimal(ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[rowIndex, colIndex + 1].Text);
                decTemp += FS.FrameWork.Function.NConvert.ToDecimal(ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[rowIndex, colIndex].Text);

                //ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[rowIndex, colIndex + 1].Text = decTemp.ToString("0.00");
                ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[rowIndex, colIndex].Text = decTemp.ToString("0.00");
            }
            ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[3, 1].Text = countMoney.ToString("0.00");
            ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[3, 3].Text =FS.FrameWork.Public.String.LowerMoneyToUpper(countMoney);        

        }


        /// <summary>
        /// 设置显示已日结项目数据
        /// </summary>
        /// <param name="table"></param>
        protected virtual void SetRePrintDetial(DataTable table)
        {
            if (table.Rows.Count == 0) return;
            //清除数据
            if (ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Rows.Count > 0)
            {
                //先清空数据，然后初始化3行
                ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Rows.Count = 0;
                ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Rows.Count = 4;
                ucClinicDayBalanceReportNew2.SetDetailName();
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

                decTemp += FS.FrameWork.Function.NConvert.ToDecimal(ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[rowIndex, colIndex].Text);

                ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[rowIndex, colIndex].Text = decTemp.ToString("0.00");

            }
            ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[3, 1].Text = countMoney.ToString("0.00");
            ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[3, 3].Text = FS.FrameWork.Public.String.LowerMoneyToUpper(countMoney);

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
        /// {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
        /// </summary>
        /// <param name="table"></param>
        protected virtual void SetInvoice(FarPoint.Win.Spread.SheetView sheet)
        {
            //获取发票数据
            DataSet ds = new DataSet();
            string strEmpID = this.currentOperator.ID;
            if (this.isAll == "0")
            {
                strEmpID = "ALL";
            }
            int resultValue = clinicDayBalance.GetDayInvoiceDataNew(strEmpID, this.lastDate, dayBalanceDate, ref ds);
            if (resultValue == -1)
            {
                MessageBox.Show(clinicDayBalance.Err);
                return;
            }

            DataTable table = ds.Tables[0];
            if (table.Rows.Count == 0)
            {
                return;
            }

            DataView dv = table.DefaultView;

            //有效票据
            dv.RowFilter = "cancel_flag='1'";
            this.SetOneCellText(sheet, EnumCellName.A002, dv.Count.ToString());

            //这里只显示作废号码

            dv.RowFilter = "cancel_flag in('0','2','3') and trans_type='2'";
            this.SetOneCellText(sheet, EnumCellName.A003, dv.Count.ToString());

            //起止票据号
            this.SetOneCellText(sheet, EnumCellName.A005, GetInvoiceStartAndEnd(table));
            FarPoint.Win.Spread.Cell cell1 = sheet.GetCellFromTag(null, EnumCellName.A005);
            string[] sTemp = cell1.Text.Split('～', '，');
            cell1.Row.Height = (float)Math.Ceiling(sTemp.Length / 5.0) * 18;

            //退费、作废票据号 
            dv.RowFilter = "cancel_flag in('0','2','3') and trans_type='2'";
            string InvoiceStr = GetInvoiceStr(dv);
            this.SetOneCellText(sheet, EnumCellName.A006, InvoiceStr);
            FarPoint.Win.Spread.Cell cell2 = sheet.GetCellFromTag(null, EnumCellName.A006);
            sTemp = cell2.Text.Split('|');
            cell2.Row.Height = (float)Math.Ceiling(sTemp.Length / 5.0) * 18;

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
            for (int i = 0; i <= count - 1; i++)
                for (int j = i + 1; j <= count; j++)
                {
                    long froInt = Convert.ToInt64(dt.Rows[i][0].ToString());
                    long nxtInt = Convert.ToInt64(dt.Rows[j][0].ToString());
                    long chaInt = nxtInt - froInt;
                    if (chaInt > 1)
                    {
                        maxStr = dt.Rows[i][0].ToString();
                        if (maxStr.Equals(minStr))
                        {
                            sb.Append(minStr + "，");
                        }
                        else
                        {
                            sb.Append(minStr + "～" + maxStr + "，");
                        }
                        minStr = dt.Rows[j][0].ToString();
                        break;
                    }
                    else
                    {
                        break;
                    }

                }
            maxStr = dt.Rows[count][0].ToString();

            if (maxStr.Equals(minStr))
            {
                sb.Append(minStr);
            }
            else
            {
                sb.Append(minStr + "～" + maxStr);
            }

            return sb.ToString();

        }

        #endregion

        /// <summary>
        /// 设置显示金额数据
        /// {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
        /// </summary>
        protected virtual void SetMoneyValue(FarPoint.Win.Spread.SheetView sheet)
        {
            decimal money = 0;
            int resultValue;
            int resultValue1;


            #region 作废金额

            decimal money1 = 0;

            //退费金额
            // {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
            string employeeID = this.currentOperator.ID;
            if (this.isAll == "0")
            {
                employeeID = "ALL";
            }

            resultValue = clinicDayBalance.GetDayBalanceCancelMoney(employeeID, this.lastDate, dayBalanceDate, ref money);
            if (resultValue != -1)
            {
                //SetOneCellText(sheet, "ddd", money.ToString());
                money1 += money;
            }
            //作废金额
            resultValue = clinicDayBalance.GetDayBalanceFalseMoney(employeeID, this.lastDate, dayBalanceDate, ref money);
            if (resultValue != -1)
            {
                //SetOneCellText(sheet, "A007", money.ToString());
                money1 += money;
            }

            SetOneCellText(sheet, EnumCellName.A004, money1.ToString());

            #endregion

            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();

            //支付方式金额
            resultValue = clinicDayBalance.GetDayBalancePayTypeMoney(employeeID, this.lastDate, dayBalanceDate, ref ds);
            if (resultValue == -1)
            {
                MessageBox.Show(clinicDayBalance.Err);
                return;
            }
            ////减免与医保金额
            //resultValue1 = clinicDayBalance.GetDayBalanceDerateAndProtectMoney(employeeID, this.lastDate, dayBalanceDate, ref ds1);
            //if (resultValue1 == -1)
            //{
            //    MessageBox.Show(clinicDayBalance.Err);
            //    return;
            //}

            resultValue = clinicDayBalance.QueryDayBalancePactPubMoney(employeeID, this.lastDate, dayBalanceDate, ref ds2);
            if (resultValue == -1)
            {
                MessageBox.Show(clinicDayBalance.Err);
                return;
            }

            DataTable dt1 = ds.Tables[0];
            DataTable dt2 = ds2.Tables[0];

            SetPayTypeValue(sheet, dt1, dt2);

            return;
      
        }

        /// <summary>
        /// 显示医保金额数据
        /// </summary>
        /// <param name="sheet">SheetView</param>
        /// <param name="ds">DataSet</param>
        protected virtual void SetProtectValue(FarPoint.Win.Spread.SheetView sheet, DataSet ds)
        {
        //    DataTable dt = ds.Tables[0];
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        switch (dr["pact_code"].ToString())
        //        {
        //            case "4"://公费
        //                {
        //                    //aaaaaaaaaaaa
        //                  //  SetOneCellText(sheet, "A012", dr["pub_cost"].ToString()); //公费医疗
        //                    this.d公费医疗 = NConvert.ToDecimal(dr["pub_cost"]);
        //                   // SetOneCellText(sheet, "A013", dr["pay_cost"].ToString());//公费自付
        //                    break;
        //                }
        //            case "2"://市护
        //                {
        //                    //SetOneCellText(sheet, "A014", dr["own_cost"].ToString());//市保自付

        //                    //SetOneCellText(sheet, "A015", dr["pay_cost"].ToString());//市保账户

        //                    //SetOneCellText(sheet, "A016", dr["pub_cost"].ToString());//市保统筹   

        //                    //SetOneCellText(sheet, "A017", dr["over_cost"].ToString());//市保大额
        //                    this.d市保账户 = NConvert.ToDecimal(dr["pay_cost"]);
        //                    this.d市保统筹 = NConvert.ToDecimal(dr["pub_cost"]);
        //                    this.d市保大额 = NConvert.ToDecimal(dr["over_cost"]);

        //                    break;
        //                }
        //            case "3"://省保
        //                {
        //                    //SetOneCellText(sheet, "A018", dr["own_cost"].ToString());//省保自付

        //                    //SetOneCellText(sheet, "A019", dr["pay_cost"].ToString());//省保账户

        //                    //SetOneCellText(sheet, "A020", dr["pub_cost"].ToString());//省保统筹

        //                    //SetOneCellText(sheet, "A021", dr["over_cost"].ToString());//省保大额

        //                    //SetOneCellText(sheet, "A022", dr["official_cost"].ToString());//省公务员
        //                    this.d省保账户 = NConvert.ToDecimal(dr["pay_cost"]);
        //                    this.d省保统筹 = NConvert.ToDecimal(dr["pub_cost"]);
        //                    this.d省保大额 = NConvert.ToDecimal(dr["over_cost"]);
        //                    this.d省保公务员 = NConvert.ToDecimal(dr["official_cost"]);

        //                    break;
        //                }
        //        }
        //    }
        }

        /// <summary>
        /// 显示公费金额数据
        /// </summary>
        /// <param name="sheet">SheetView</param>
        /// <param name="ds">DataSet</param>
        protected virtual void SetPublicValue(FarPoint.Win.Spread.SheetView sheet, DataSet ds)
        {
        //    DataTable dt = ds.Tables[0];
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        //SetOneCellText(sheet, "A012", dr["pub_cost"].ToString()); //公费医疗
        //        //this.d公费医疗 = NConvert.ToDecimal(dr["pub_cost"]);
        //        //SetOneCellText(sheet, "A013", dr["own_cost"].ToString());//公费自付  
        //        //SetOneCellText(sheet, "A026", dr["pay_cost"].ToString());//公费账户
        //        this.d公费账户 = NConvert.ToDecimal(dr["pay_cost"]);
        //        //SetOneCellText(sheet, "A010", dr["rebate_cost"].ToString());
        //        //this.d减免金额 = NConvert.ToDecimal(dr["rebate_cost"]);
        //    }
        }

        //{0EA3CF1A-2F03-46c3-83AE-1543B00BBDDB}
        /// <summary>
        /// 显示减免金额数据
        /// </summary>
        /// <param name="sheet">SheetView</param>
        /// <param name="ds">DataSet</param>
        protected virtual void SetRebateValue(FarPoint.Win.Spread.SheetView sheet, DataSet ds)
        {
        //    DataTable dt = ds.Tables[0];
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        SetOneCellText(sheet, EnumCellName.A010, dr["rebate_cost"].ToString());
        //        this.d减免金额 = NConvert.ToDecimal(dr["rebate_cost"]);
        //    }
        }

        /// <summary>
        /// 按支付类型显示金额
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="dtPayMode">支付方式汇总</param>
        /// <param name="dtPactEco">减免汇总</param>
        /// <param name="dtPactFSSI">特定门诊汇总</param>
        protected virtual void SetPayTypeValue(FarPoint.Win.Spread.SheetView sheet, DataTable dtPayMode, DataTable dtPactFSSI)
        {
            string payMode = string.Empty;
            string pact = string.Empty;
            decimal detailCost = 0;
            //减免金额
            decimal derateCost = 0;
            //医保金额
            decimal protectCost = 0;

            //现金
            decimal CAcost = 0;
            //刷卡
            decimal CDcost = 0;
            //账户
            decimal PScost = 0;
            #region 不用
            ////特殊门诊
            //decimal TSMZ = 0;
            ////老人减免
            //decimal LRJM = 0;
            ////合作医疗
            //decimal HZYL = 0;
            ////公务员
            //decimal GWY = 0;
            ////优惠尤特
            //decimal YHYT = 0;
            ////居民医保
            //decimal JMYB = 0;
            ////本院减免
            //decimal BYJM = 0;
            #endregion
            //职工基本医疗
            decimal ZGYL = 0;
            //居民基本医疗
            decimal JMYL = 0;
            //特约单位
            decimal TYDW = 0;
            //医疗优惠
            decimal YLYH = 0;
            //特定门诊
            decimal TDMZ = 0;

            #region 支付方式
            foreach (DataRow dr in dtPayMode.Rows)
            {
                pact = dr[0].ToString();
                payMode = dr[1].ToString();
                detailCost = FS.FrameWork.Function.NConvert.ToDecimal(dr[2]);

                if (payMode == "CA")
                {
                    CAcost += detailCost;
                }
                else if (payMode == "DB" || payMode == "CD")
                {
                    CDcost += detailCost;
                }
                //账户支付方式先当做现金支付
                else if (payMode == "YS")
                {
                    PScost += detailCost;
                }

                #region 不用
                //// 减免支付
                //if (payMode == "RC")
                //{
                //    //if (pact == "2" || pact == "9" || pact == "10" || pact == "11")
                //    //{
                //    //    // 特殊门诊报销
                //    //    TSMZ += protectCost;
                //    //}

                //    //if (pact == "3" || pact == "4" || pact == "5" || pact == "6")
                //    //{
                //    //    // 居民医保报销
                //    //    JMYB += protectCost;
                //    //}

                //    if (pact == "6" || pact == "7" || pact == "10")
                //    {
                //        // 老年减免
                //        LRJM += detailCost;
                //    }

                //    if (pact == "4")
                //    {
                //        // 合作医疗 减免
                //        HZYL += detailCost;
                //    }

                //    if (pact == "5" || pact == "8" || pact == "11")
                //    {
                //        // 优抚减免
                //        YHYT += detailCost;
                //    }

                //    if (pact == "9")
                //    {
                //        // 离休干部 -- 本院减免
                //        BYJM += detailCost;
                //    }
                //}
                #endregion

            }
            #endregion

            #region 减免汇总
            //foreach (DataRow dr1 in dtPactEco.Rows)
            //{
            //    pact = dr1[0].ToString();
            //    derateCost = FS.FrameWork.Function.NConvert.ToDecimal(dr1[1]);
            //    protectCost = FS.FrameWork.Function.NConvert.ToDecimal(dr1[2]);

            //    if (pact == "2" || pact == "9" || pact == "10" || pact == "11")
            //    {
            //        // 特殊门诊报销
            //        TSMZ += protectCost;
            //    }

            //    if (pact == "3" || pact == "4" || pact == "5" || pact == "6")
            //    {
            //        // 居民医保报销
            //        JMYB += protectCost;
            //    }

            //    if (pact == "6" || pact == "7" || pact == "10")
            //    {
            //        // 老年减免
            //        LRJM += derateCost;
            //    }

            //    if (pact == "4")
            //    {
            //        // 合作医疗 减免
            //        HZYL += derateCost;
            //    }

            //    if (pact == "5" || pact == "8" || pact == "11")
            //    {
            //        // 优抚减免
            //        YHYT += derateCost;
            //    }

            //    if (pact == "9")
            //    {
            //        // 离休干部 -- 本院减免
            //        BYJM += derateCost;
            //    }



            //    //if (pact == "2" )
            //    //{
            //    //    TSMZ += protectCost;
            //    //}
            //    //else if (pact == "3")
            //    //{
            //    //    JMYB += protectCost;
            //    //}
            //    //else if (pact == "4")
            //    //{
            //    //    HZYL += derateCost;
            //    //    JMYB += protectCost;
            //    //}
            //    //else if (pact == "5")
            //    //{
            //    //    YHYT += derateCost;
            //    //    JMYB += protectCost;
            //    //}
            //    //else if (pact == "6")
            //    //{
            //    //    LRJM += derateCost;
            //    //    JMYB += protectCost;
            //    //}
            //    //else if (pact == "7")
            //    //{
            //    //    LRJM += derateCost;
            //    //    JMYB += protectCost;
            //    //}
            //    //else if (pact == "8")
            //    //{
            //    //    YHYT += derateCost;
            //    //    JMYB += protectCost;
            //    //}
            //}
            #endregion

            #region 特定门诊/居发门诊报销

            foreach (DataRow drTemp in dtPactFSSI.Rows)
            {
                pact = drTemp[0].ToString();
                protectCost = FS.FrameWork.Function.NConvert.ToDecimal(drTemp[1]);

                if (pact == "2")
                {
                    // 职工医疗保险
                    ZGYL += protectCost;
                }
                else if (pact == "3")
                {
                    // 居民医保报销
                    JMYL += protectCost;
                }
                else if (pact == "6")
                {
                    // 医疗优惠
                    YLYH += protectCost;
                }
                else if (pact == "7")
                {
                    // 特定门诊
                    TDMZ += protectCost;
                }
                else if (pact == "8" || pact == "9" || pact == "10")
                {
                    // 特约单位
                    TYDW += protectCost;
                }
                else
                {
                    // 医疗优惠
                    YLYH += protectCost;
                }
            }

            #endregion
            //如果是日结赋值在日结界面，如果是查询重打则赋值在查询重打界面
            if (sheet == this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1)
            {
                this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[9, 3].Text = CDcost.ToString("0.00");
                this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[9, 5].Text = CAcost.ToString("0.00");
                this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[4, 1].Text = FS.FrameWork.Public.String.LowerMoneyToUpper(CAcost);
                this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[8, 1].Text = ZGYL.ToString("0.00");
                this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[8, 3].Text = JMYL.ToString("0.00");
                this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[8, 5].Text = TYDW.ToString("0.00");
                this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[8, 7].Text = YLYH.ToString("0.00");
                #region 先不统计公务员减免
                //this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[6, 6].Text = "";
                //this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[6, 7].Text = "";
                #endregion
                this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[9, 1].Text = TDMZ.ToString("0.00");
                //this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Rows[4].Visible = false;
                //this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Rows[5].Visible = false;
            }
            else
            {
                this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[9, 3].Text = CDcost.ToString("0.00");
                this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[9, 5].Text = CAcost.ToString("0.00");
                this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[4, 1].Text = FS.FrameWork.Public.String.LowerMoneyToUpper(CAcost);
                this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[8, 1].Text = ZGYL.ToString("0.00");
                this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[8, 3].Text = JMYL.ToString("0.00");
                this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[8, 5].Text = TYDW.ToString("0.00");
                this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[8, 7].Text = YLYH.ToString("0.00");
                #region 先不统计公务员减免
                //this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[6, 6].Text = "";
                //this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[6, 7].Text = "";
                #endregion
                this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[9, 1].Text = TDMZ.ToString("0.00");
                //this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[7, 3].Text = JMYB.ToString("0.00");
                //this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[7, 5].Text = BYJM.ToString("0.00");
                //this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Rows[6].Visible = false;
                //this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Rows[7].Visible = false;
            }
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
            ArrayList balanceRecord = new ArrayList();
            // 查询的日记流水号
            string sequence = "";

            ////清除数据
            //int count = this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Rows.Count;
            //if (count > 0)
            //{
            //    this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Rows.Remove(0, count);
            //}

            // 获取查询时间
            intReturn = this.ucReprintDateControl1.GetInputDateTime(ref dtFrom, ref dtTo);
            if (intReturn == -1)
            {
                return;
            }

            // 获取查询结果
            // {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
            if (this.isAll == "0")
            {
                intReturn = this.clinicDayBalance.GetBalanceRecord(this.empBalance, dtFrom, dtTo, ref balanceRecord);
            }
            else
            {
                intReturn = this.clinicDayBalance.GetBalanceRecord(this.currentOperator, dtFrom, dtTo, ref balanceRecord);
            }
            if (intReturn == -1)
            {
                MessageBox.Show("获取日志记录失败");
                return;
            }

            string begin = string.Empty, end = string.Empty;

            // 判断结果记录数，如果多条，那么弹出窗口让用户选择
            if (balanceRecord.Count > 1)
            {
                frmConfirmBalanceRecord confirmBalanceRecord = new frmConfirmBalanceRecord();
                confirmBalanceRecord.BalanceRecord = balanceRecord;
                if (confirmBalanceRecord.ShowDialog() == DialogResult.OK)
                {
                    sequence = confirmBalanceRecord.fpSpread1.Sheets[0].Cells[confirmBalanceRecord.fpSpread1.Sheets[0].ActiveRowIndex, 0].Text;
                    begin = confirmBalanceRecord.fpSpread1.Sheets[0].Cells[confirmBalanceRecord.fpSpread1.Sheets[0].ActiveRowIndex, 1].Text;
                    end = confirmBalanceRecord.fpSpread1.Sheets[0].Cells[confirmBalanceRecord.fpSpread1.Sheets[0].ActiveRowIndex, 2].Text;
                }
                else
                {
                    return;
                }
            }
            else if (balanceRecord.Count < 1)
            {
                MessageBox.Show("该时间段内没有要查找的数据！");
                return;
            }
            else
            {
                foreach (NeuObject obj in balanceRecord)
                {
                    sequence = obj.ID;
                    begin = obj.Name;
                    end = obj.Memo;
                }
            }
            //通过查询当时日结开始时间和结束时间来实现重打
            this.lastDate = begin.ToString();
            this.dayBalanceDate = end.ToString();
            //显示报表信息
            this.ucClinicDayBalanceReportNew2.Clear(lastDate, dayBalanceDate);
            DataSet ds = new DataSet();
            // {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
            string strEmpID = this.currentOperator.ID;
            if (this.isAll == "0")
            {
                strEmpID = "ALL";
            }
            clinicDayBalance.GetDayBalanceDataMZRJ(strEmpID, this.lastDate, this.dayBalanceDate, ref ds);

            this.SetRePrintDetial(ds.Tables[0]);

            //设置farpoint格式
            this.ucClinicDayBalanceReportNew2.SetFarPoint();

            //显示发票汇总数据
            SetInvoice(this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1);

            //显示金额数据
            this.SetMoneyValue(this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1);
            ds.Dispose();
            ////查找日结数据
            //DataSet ds = new DataSet();
            //intReturn = clinicDayBalance.GetDayBalanceRecord(sequence, ref ds);
            //if (intReturn == -1)
            //{
            //    MessageBox.Show(clinicDayBalance.Err);
            //    return;
            //}
            //if (ds.Tables.Count == 0 || ds == null || ds.Tables[0].Rows.Count == 0)
            //{
            //    MessageBox.Show("该时间段内没有要查找的数据！");
            //    return;
            //}
            //SetOldFarPointData(ds.Tables[0]);
            //ds.Dispose();

        }

        #endregion

        #region 设置已日结Farpoint数据
        private void SetOldFarPointData(DataTable table)
        {
            FarPoint.Win.Spread.SheetView sheet = this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1;
            int rowCount = sheet.Rows.Count;
            if (sheet.Rows.Count > 0)
            {
                sheet.Rows.Remove(0, rowCount - 1);
            }
            DataView dv = table.DefaultView;
            //设置项目明细
            SetDetialed(sheet, dv);
            this.ucClinicDayBalanceReportNew2.SetFarPoint();
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

        /// <summary>
        /// 设置单个金额实体（只存一个金额或数量）
        /// </summary>
        /// <param name="InvoiceID">统计大类编码</param>
        /// <param name="InvoiceName">统计大类名称</param>
        /// <param name="Money">金额</param>
        /// <param name="typeStr">类别</param>
        private void SetOneCellDayBalance(string InvoiceID, string InvoiceName, decimal Money, string typeStr)
        {
            dayBalance = new Models.ClinicDayBalanceNew();
            dayBalance.InvoiceNO.ID = InvoiceID;
            dayBalance.InvoiceNO.Name = InvoiceName;
            dayBalance.TotCost = Money;
            dayBalance.TypeStr = typeStr;
            dayBalance.SortID = InvoiceID + "|" + "TOT_COST";
            AddDayBalanceToArray();
        }

        #endregion

        #region 保存日结数据
        /// <summary>
        /// 保存日结数据
        /// </summary>
        public void DayBalance()
        {
            //if (this.alData == null || this.alData.Count == 0)
            //{
            //    return;
            //}

            if (MessageBox.Show("是否进行日结,日结后数据将不能恢复?", "门诊收款员缴款日报", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
               

                // 等待窗口
                FS.FrameWork.WinForms.Forms.frmWait waitForm = new FS.FrameWork.WinForms.Forms.frmWait();
               
               

                if (this.alData == null)
                {
                    return;
                }
                // 启动等待窗口
                waitForm.Tip = "正在进行日结";
                waitForm.Show();

                string strEmpID = this.currentOperator.ID;
                string strOperID = this.currentOperator.ID;
                if (this.isAll == "0")
                {
                    strEmpID = "ALL";
                    strOperID = this.empBalance.ID;
                }
               
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                int iRes = outpatient.DealOperDayBalance(strEmpID, strOperID, this.lastDate, this.dayBalanceDate);
                if (iRes < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    waitForm.Hide();
                    MessageBox.Show("日结出错！");
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                waitForm.Hide();
                MessageBox.Show("日结成功完成");
                PrintInfo(this.neuPanel1);
                alData.Clear();
                this.InitUC();
                this.ucClinicDayBalanceDateControl1.dtpBalanceDate.Value = this.ucClinicDayBalanceDateControl1.dtLastBalance;
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
                if (this.neuTabControl1.SelectedIndex == 1)
                {
                    MessageBox.Show("重打界面不可以日结!");
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
                        this.PrintInfo(this.neuPanel1);
                        break;
                    }
                case 1:
                    {
                        //{C1A4AEEB-6A47-4208-B6EE-6634B00840FD}
                        //MessageBox.Show(this.panelPrint.Controls.Count.ToString());
                        this.PrintInfo(this.panelPrint);

                        break;
                    }
            }

            return base.OnPrint(sender, neuObject);
        }

        protected virtual void PrintInfo(FS.FrameWork.WinForms.Controls.NeuPanel panelPrint)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            //{C1A4AEEB-6A47-4208-B6EE-6634B00840FD
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.PrintPage(0, 0, panelPrint);
        }

        #endregion
    }
}
