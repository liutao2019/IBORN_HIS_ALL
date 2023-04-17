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
using SOC.Fee.DayBalance.Object;

namespace SOC.Fee.DayBalance.Inpatient.QiaoTou
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
        public List<Object.InpatientDayBalance> alData = new List<Object.InpatientDayBalance>();

        private Object.InpatientDayBalance dayBalance = null;

        /// <summary>
        /// 统计大类业务类
        /// </summary>
        FS.HISFC.BizLogic.Fee.FeeCodeStat feecodeStat = new FS.HISFC.BizLogic.Fee.FeeCodeStat();

        /// <summary>
        /// 打印纸张设置类
        /// </summary>
        FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();

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
        SOC.Fee.DayBalance.Manager.InpatientDayBalanceManage inpatientDayBalanceManage = new SOC.Fee.DayBalance.Manager.InpatientDayBalanceManage();

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

        #region 统计支付方式 

        List<string> lstTitle = null;        //标题
        List<string> lstStaticType = null;   //pay 或者 pub
        List<string> lstPayMode = null;      //支付方式
        List<List<string>> lstPact = null;   //合同单位
        List<decimal> lstValue = null;       //金额

        string strSetting;

        /// <summary>
        /// 设置日结统计项目，及统计方式
        /// </summary>
        [Category("设置"), Description("设置日结统计项目，及统计方式")]
        public string StrSetting
        {
            get
            { 
                return strSetting; 
            }

            set
            {
                strSetting = value;

                try
                {
                    string[] strArr = strSetting.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (strArr == null)
                    {
                        lstTitle = new List<string>();
                        lstStaticType = new List<string>();
                        lstPayMode = new List<string>();
                        lstPact = new List<List<string>>();
                        lstValue = new List<decimal>();
                    }
                    else
                    {
                        int iLen = strArr.Length;
                        lstTitle = new List<string>();
                        lstStaticType = new List<string>();
                        lstPayMode = new List<string>();
                        lstPact = new List<List<string>>();
                        lstValue = new List<decimal>();

                        string strTemp = null;
                        string[] strTempArr = null;
                        for (int idx = 0; idx < iLen; idx++)
                        {
                            strTemp = strArr[idx];
                            strTempArr = strTemp.Split(new char[] { '|' });

                            lstTitle.Add(strTempArr[0]);
                            lstStaticType.Add(strTempArr[1]);
                            lstPayMode.Add(strTempArr[2]);

                            if (string.IsNullOrEmpty(strTempArr[3]) || strTempArr[3] == "ALL")
                            {
                                lstPact.Add(new List<string>());
                            }
                            else
                            {
                                List<string> lst = new List<string>();
                                lst.AddRange(strTempArr[3].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

                                lstPact.Add(lst);
                            }

                            lstValue.Add(0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

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

        string strStatClass = string.Empty;

        /// <summary>
        /// 显示统计大类
        /// </summary>
        [Category("设置"), Description("显示统计大类")]
        public string StatClass
        {
            get { return strStatClass; }
            set { strStatClass = value; }
        }

        /// <summary>
        /// 上交金额
        /// </summary>
        string strCACost = string.Empty;
        /// <summary>
        /// 上交金额
        /// </summary>
        [Category("设置"), Description("上交金额")]
        public string CACost
        {
            get { return strCACost; }
            set { strCACost = value; }
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
                if (this.isAll == "1")
                {
                    this.currentOperator = this.inpatientDayBalanceManage.Operator;
                }
                else
                {
                    this.currentOperator = new NeuObject();
                    this.currentOperator.ID = "T00001";
                    this.currentOperator.Name = "T-全院";

                }

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

                List<FS.HISFC.Models.Fee.FeeCodeStat> lstFeecodeStat = null;
                if (!string.IsNullOrEmpty(strStatClass))
                {
                    lstFeecodeStat = this.feecodeStat.QueryFeeStatNameByReportCode(strStatClass);
                }

                this.ucInpatientDayBalanceReportNew1.lstFeecodeStat = lstFeecodeStat;   //日结
                this.ucInpatientDayBalanceReportNew2.lstFeecodeStat = lstFeecodeStat;   //日结重打

                // 初始化子控件的变量
                this.ucInpatientDayBalanceDateControl1.dtLastBalance = NConvert.ToDateTime(this.lastDate);
                this.ucInpatientDayBalanceReportNew1.InitUC(this.inpatientDayBalanceManage.Hospital.Name + reportTitle);
                this.ucInpatientDayBalanceReportNew2.InitUC(this.inpatientDayBalanceManage.Hospital.Name + reportTitle);
                //this.ucInpatientDayBalanceReportNew1.SetDetailName();

                this.ucInpatientDayBalanceReportNew1.ClearALL("", "", lstTitle);
                this.ucInpatientDayBalanceReportNew2.ClearALL("", "", lstTitle);

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
            if (isAll == "0")
            {
                strEmpID = "ALL";
            }

            //统计发票大类的金额
            DataTable dtCostStat = null;
            this.inpatientDayBalanceManage.QueryDayBalanceCostByStat(strEmpID, this.lastDate, dayBalanceDate, out dtCostStat);

            this.SetDetial(dtCostStat);


            //设置farpoint格式
            this.ucInpatientDayBalanceReportNew1.SetFarPoint(this.lstTitle);

            //显示发票汇总数据
            SetInvoice(this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1);

            

            //显示发票起止号，张数相关信息
            //DataTable dtInvoice = null;
            //this.inpatientDayBalanceManage.QueryDayBalanceInvoiceDetial(strEmpID, this.lastDate, dayBalanceDate, out dtInvoice);
            //SetInvoiceBeginAndEnd(dtInvoice, this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1);

            //显示金额数据
            this.SetMoneyValue(this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1);
            return;
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
                sortID = FS.FrameWork.Function.NConvert.ToInt32(table.Rows[i][3]) - 1;
                int rowIndex = Convert.ToInt32(sortID % 3);
                int colIndex = Convert.ToInt32(sortID / 3) * 2;

                decTemp = FS.FrameWork.Function.NConvert.ToDecimal(table.Rows[i][2]);
                countMoney += decTemp;

                decTemp += FS.FrameWork.Function.NConvert.ToDecimal(ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[rowIndex, colIndex + 1].Text);

                ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[rowIndex, colIndex + 1].Text = decTemp.ToString("0.00");

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
                sortID = FS.FrameWork.Function.NConvert.ToInt32(table.Rows[i][3]) - 1;
                int rowIndex = Convert.ToInt32(sortID % 3);
                int colIndex = Convert.ToInt32(sortID / 3) * 2;

                decTemp = FS.FrameWork.Function.NConvert.ToDecimal(table.Rows[i][2]);
                countMoney += decTemp;

                decTemp += FS.FrameWork.Function.NConvert.ToDecimal(ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[rowIndex, colIndex + 1].Text);

                ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[rowIndex, colIndex + 1].Text = decTemp.ToString("0.00");

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
            //获取发票数据
            string strEmpID = this.currentOperator.ID;
            if (this.isAll == "0")
            {
                strEmpID = "ALL";
            }

            DataSet dsInvoice = new DataSet();
            int valueReturn = this.inpatientDayBalanceManage.GetDayInvoiceDataNew(strEmpID, this.lastDate, dayBalanceDate, ref dsInvoice);
            if (valueReturn == -1)
            {
                MessageBox.Show(inpatientDayBalanceManage.Err);
                return;
            }

            DataTable table = dsInvoice.Tables[0];
            if (table.Rows.Count <= 0)
            {
                return;
            }

            DataView dv = table.DefaultView;

            //有效票据号
            dv.RowFilter = "WASTE_FLAG = '1'";
            this.SetOneCellText(sheet, EnumCellName.A002, dv.Count.ToString());

            //退费票据号
            dv.RowFilter = "WASTE_FLAG = '0' and TRANS_TYPE = '2'";
            this.SetOneCellText(sheet, EnumCellName.A003, dv.Count.ToString());

            //起始号码段
            dv.RowFilter = "TRANS_TYPE = '1'";
            this.SetOneCellText(sheet, EnumCellName.A014, this.GetInvoiceStartAndEnd(dv));

            //退费票据号
            dv.RowFilter = "WASTE_FLAG = '0' and TRANS_TYPE = '2'";
            this.SetOneCellText(sheet, EnumCellName.A015, this.GetInvoiceStr(dv));

            return;

            #region 废弃
            //string effectiveBill = string.Empty;
            //string uneffectiveBill = string.Empty;
            //int resultValue = this.inpatientDayBalanceManage.QueryDayBalanceEffectiveBill(strEmpID, this.lastDate, dayBalanceDate, out effectiveBill);
            //if (resultValue == -1)
            //{
            //    MessageBox.Show(this.inpatientDayBalanceManage.Err);
            //    return;
            //}

            //this.SetOneCellText(sheet, EnumCellName.A002, effectiveBill);

            ////这里只显示作废号码
            //resultValue = this.inpatientDayBalanceManage.QueryDayBalanceUneffectiveBill(strEmpID, this.lastDate, dayBalanceDate, out uneffectiveBill);
            //if (resultValue == -1)
            //{
            //    MessageBox.Show(this.inpatientDayBalanceManage.Err);
            //    return;
            //}

            //this.SetOneCellText(sheet, EnumCellName.A003, uneffectiveBill);
            #endregion

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
        /// 查询并显示日结之后的发票数据
        /// </summary>
        /// <param name="table"></param>
        protected virtual void SetInvoiceReprint(FarPoint.Win.Spread.SheetView sheet)
        {
            //获取发票数据
            string strEmpID = this.currentOperator.ID;
            if (this.isAll == "0")
            {
                strEmpID = "ALL";
            }

            DataSet dsInvoice = new DataSet();
            int valueReturn = this.inpatientDayBalanceManage.GetDayInvoiceDataNewReprint(strEmpID, this.lastDate, dayBalanceDate, ref dsInvoice);
            if (valueReturn == -1)
            {
                MessageBox.Show(inpatientDayBalanceManage.Err);
                return;
            }

            DataTable table = dsInvoice.Tables[0];
            if (table.Rows.Count <= 0)
            {
                return;
            }

            DataView dv = table.DefaultView;

            //有效票据号
            dv.RowFilter = "WASTE_FLAG = '1'";
            this.SetOneCellText(sheet, EnumCellName.A002, dv.Count.ToString());

            //退费票据号
            dv.RowFilter = "WASTE_FLAG = '0' and TRANS_TYPE = '2'";
            this.SetOneCellText(sheet, EnumCellName.A003, dv.Count.ToString());

            //起始号码段
            dv.RowFilter = "TRANS_TYPE = '1'";
            this.SetOneCellText(sheet, EnumCellName.A014, this.GetInvoiceStartAndEnd(dv));

            //退费票据号
            dv.RowFilter = "WASTE_FLAG = '0' and TRANS_TYPE = '2'";
            this.SetOneCellText(sheet, EnumCellName.A015, this.GetInvoiceStr(dv));

            return;


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

        

        #region 获得起始、终止票据号

        private string GetInvoiceStartAndEnd(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            int count = dt.Rows.Count - 1;
            string minStr = dt.Rows[0][0].ToString();
            string maxStr = dt.Rows[0][0].ToString();
            for (int i = 0; i < count - 1; i++)
                for (int j = i + 1; j < count; j++)
                {
                    long froInt = Convert.ToInt32(dt.Rows[i][0].ToString());
                    long nxtInt = Convert.ToInt32(dt.Rows[j][0].ToString());
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
            maxStr = dt.Rows[count][0].ToString();
            sb.Append(minStr + "～" + maxStr);
            return sb.ToString();

        }

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
                                long cTemp = nTemp - fTemp ;
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
                                long cTemp = nTemp - fTemp ;
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

        private string GetPrintInvoiceno(DataRowView drv)
        {
            string str = drv["print_invoiceno"].ToString();
            str = str.TrimStart(new char[] { '0' });
            str = str.PadLeft(8, '0');

            return str;
        }

        /// <summary>
        /// 获得作废、退费票据号
        /// </summary>
        /// <param name="dv">DataView</param>
        /// <param name="aMod">作废还是退费1是作废 0是退费</param>
        /// <returns></returns>
        private string GetInvoiceStr(DataView dv)
        {
            if (dv != null && dv.Count != 0)
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
                        sb.Append(GetPrintInvoiceno(dv[i]) + "|");

                    }
                }
                return sb.ToString();
            }
            else
            {
                return "";
            }
        }

        #endregion

        /// <summary>
        /// 设置显示金额数据
        /// </summary>
        protected virtual void SetMoneyValue(FarPoint.Win.Spread.SheetView sheet)
        {
            int resultValue;

            string employeeID = this.currentOperator.ID;

            if (this.isAll == "0")
            {
                employeeID = "ALL";
            }

            //退费金额
            decimal cancelMoney = 0m;
            resultValue = this.inpatientDayBalanceManage.GetDayBalanceCancelMoney(employeeID, this.lastDate, this.dayBalanceDate, ref cancelMoney);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }

            this.SetOneCellText(sheet, EnumCellName.A004, cancelMoney.ToString());

            //冲销预交金金额
            decimal prepayMoney = 0m;
            resultValue = this.inpatientDayBalanceManage.QueryDayBalancePrepayMoney(employeeID, this.lastDate, this.dayBalanceDate, ref prepayMoney);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }
            this.SetOneCellText(sheet, EnumCellName.A010, prepayMoney.ToString());

            //支付方式金额
            DataSet dsPay = new DataSet();
            resultValue = this.inpatientDayBalanceManage.GetDayBalancePayTypeMoney(employeeID, this.lastDate, this.dayBalanceDate, ref dsPay);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }

            //社保金额
            DataSet dsPub = new DataSet();
            resultValue = this.inpatientDayBalanceManage.QueryDayBalancePactPubMoney(employeeID, this.lastDate, this.dayBalanceDate, ref dsPub);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }



            //DataTable dtBillMoney = null;

            //resultValue = this.inpatientDayBalanceManage.QueryDayBalanceBillMoney(employeeID, this.lastDate, dayBalanceDate, out dtBillMoney);

            SetPayTypeValue(sheet, dsPay.Tables[0], dsPub.Tables[0]);

            return;

        }

        /// <summary>
        /// 设置显示日结之后的金额数据
        /// </summary>
        protected virtual void SetMoneyValueReprint(FarPoint.Win.Spread.SheetView sheet)
        {
            int resultValue;

            string employeeID = this.currentOperator.ID;

            if (this.isAll == "0")
            {
                employeeID = "ALL";
            }

            //退费金额
            decimal cancelMoney = 0m;
            resultValue = this.inpatientDayBalanceManage.GetDayBalanceCancelMoneyReprint(employeeID, this.lastDate, this.dayBalanceDate, ref cancelMoney);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }

            this.SetOneCellText(sheet, EnumCellName.A004, cancelMoney.ToString());

            //冲销预交金金额
            decimal prepayMoney = 0m;
            resultValue = this.inpatientDayBalanceManage.QueryDayBalancePrepayMoneyReprint(employeeID, this.lastDate, this.dayBalanceDate, ref prepayMoney);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }
            this.SetOneCellText(sheet, EnumCellName.A010, prepayMoney.ToString());

            //支付方式金额
            DataSet dsPay = new DataSet();
            resultValue = this.inpatientDayBalanceManage.GetDayBalancePayTypeMoneyReprint(employeeID, this.lastDate, this.dayBalanceDate, ref dsPay);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }

            //社保金额
            DataSet dsPub = new DataSet();
            resultValue = this.inpatientDayBalanceManage.QueryDayBalancePactPubMoneyReprint(employeeID, this.lastDate, this.dayBalanceDate, ref dsPub);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }



            //DataTable dtBillMoney = null;

            //resultValue = this.inpatientDayBalanceManage.QueryDayBalanceBillMoney(employeeID, this.lastDate, dayBalanceDate, out dtBillMoney);

            SetPayTypeValue(sheet, dsPay.Tables[0], dsPub.Tables[0]);

            return;

        }

        /// <summary>
        /// 按类型显示金额
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="dtPayMode">支付方式</param>
        /// <param name="dtPactFSSI">社保金额</param>
        protected virtual void SetPayTypeValue(FarPoint.Win.Spread.SheetView sheet, DataTable dtPayMode, DataTable dtPactFSSI)
        {

            string payMode = string.Empty;
            string pact = string.Empty;

            decimal detailCost = 0;

            if (lstValue != null && lstValue.Count > 0)
            {
                for (int idx = 0; idx < lstValue.Count; idx++)
                {
                    lstValue[idx] = 0;
                }
            }

            decimal protectCost = 0;

            #region 统计各项数值
            int iCount = lstTitle.Count;
            for (int idx = 0; idx < iCount; idx++)
            {
                if (lstStaticType[idx] == "pay")
                {
                    foreach (DataRow dr in dtPayMode.Rows)
                    {
                        pact = dr[0].ToString();
                        payMode = dr[1].ToString();
                        detailCost = FS.FrameWork.Function.NConvert.ToDecimal(dr[2]);

                        if (lstPayMode[idx].Contains(payMode))
                        {
                            if (lstPact[idx] == null || lstPact[idx].Count <= 0)
                            {
                                lstValue[idx] += detailCost;
                            }
                            else if (lstPact[idx].Contains(pact))
                            {
                                lstValue[idx] += detailCost;
                            }
                        }
                    }
                }
                else if (lstStaticType[idx] == "pub")
                {
                    // 医保报销
                    foreach (DataRow drTemp in dtPactFSSI.Rows)
                    {
                        pact = drTemp[0].ToString();
                        protectCost = FS.FrameWork.Function.NConvert.ToDecimal(drTemp[1]);

                        if (lstPact[idx].Contains(pact))
                        {
                            lstValue[idx] += protectCost;
                        }
                    }
                }
            }

            // 上交金额
            if (!string.IsNullOrEmpty(strCACost))
            {
                if (lstTitle.Contains(strCACost))
                {
                    decimal decCA = lstValue[lstTitle.IndexOf(strCACost)];
                    sheet.Cells[4, 1].Text = decCA.ToString();
                    sheet.Cells[4, 3].Text = FS.FrameWork.Public.String.LowerMoneyToUpper(lstValue[lstTitle.IndexOf(strCACost)]);
                    this.SetOneCellText(sheet, EnumCellName.A013, decCA.ToString());
                }
            }

            #endregion

            int iCellCount = 8;
            string strTemp = "";
            int iTitleIdx = 0;

            for (int iRow = 0; iRow < sheet.RowCount; iRow++)
            {
                for (int iCell = 0; iCell < iCellCount; iCell++)
                {
                    if (sheet.Cells[iRow, iCell].Tag == null)
                    {
                        continue;
                    }
                    strTemp = sheet.Cells[iRow, iCell].Tag.ToString();
                    if (string.IsNullOrEmpty(strTemp) || !lstTitle.Contains(strTemp))
                    {
                        continue;
                    }

                    iTitleIdx = lstTitle.IndexOf(strTemp);

                    sheet.Cells[iRow, iCell].Text = lstValue[iTitleIdx].ToString();

                }

            }

            #region 废弃
            //// 职工医保
            //decimal decZGYB = 0;
            //// 老年减免
            //decimal decLNJM = 0;
            //// 合作医疗
            //decimal decHZYL = 0;
            //// 优惠优待
            //decimal decYHYD = 0;
            //// 居民医保
            //decimal decJMYB = 0;
            //// 本院减免
            //decimal decBYJM = 0;
            //// 预交金结算
            //decimal decYJJJS = 0;
            //// 现金结算
            //decimal decCA = 0;
            ////上缴金额
            //decimal decSJJE = 0;

            //// 职工医保
            //string strPact = "2|9|10|11|12";

            //decZGYB = this.ComputeByPact(strPact, "Sum(pubcost)", dtBillMoney);

            //// 老年减免
            //// 合作医疗
            //// 优惠优待
            //// 本院减免
            //// 暂不处理

            //// 居民医保
            //strPact = "3|4|5|6";
            //decJMYB = this.ComputeByPact(strPact, "Sum(pubcost)", dtBillMoney);

            //decYJJJS = FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Compute("Sum(prepaycost)", ""));

            //decCA = FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Compute("Sum(supplycost)", "")) - FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Compute("Sum(returncost)", ""));

            //decSJJE = decYJJJS + decCA;

            //FarPoint.Win.BevelBorder bevelBorder1 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 2, false, false, false, true);

            ////如果是日结赋值在日结界面，如果是查询重打则赋值在查询重打界面
            //string strTemp = string.Empty;
            //if (sheet == this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1)
            //{
            //    strTemp = FS.FrameWork.Public.String.LowerMoneyToUpper(decSJJE);
            //    if (decSJJE < 0)
            //    {
            //        strTemp = "负" + strTemp;
            //    }
            //    this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Rows[7].Border = bevelBorder1;
            //    this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[7, 1].Text = decSJJE.ToString("0.00");

            //    this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[7, 3].Text = strTemp;

            //    this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[5, 1].Text = decZGYB.ToString("0.00");

            //    this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[6, 3].Text = decJMYB.ToString("0.00");

            //    this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[8, 1].Text = decYJJJS.ToString("0.00");

            //    this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[8, 3].Text = decCA.ToString("0.00");

            //    this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Rows[4].Visible = false;
            //}
            //else
            //{
            //    strTemp = FS.FrameWork.Public.String.LowerMoneyToUpper(decSJJE);
            //    if (decSJJE < 0)
            //    {
            //        strTemp = "负 " + strTemp;
            //    }
            //    this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Rows[7].Border = bevelBorder1;

            //    this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[7, 1].Text = decSJJE.ToString("0.00");

            //    this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[7, 3].Text = strTemp;

            //    this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[5, 1].Text = decZGYB.ToString("0.00");

            //    this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[6, 3].Text = decJMYB.ToString("0.00");

            //    this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[8, 1].Text = decYJJJS.ToString("0.00");

            //    this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[8, 3].Text = decCA.ToString("0.00");

            //    this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Rows[4].Visible = false;
            //}
            #endregion

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
                MessageBox.Show("获取日志记录失败");
                return;
            }

            string begin = string.Empty, end = string.Empty;

            // 判断结果记录数，如果多条，那么弹出窗口让用户选择
            if (lstBalanceRecord == null || lstBalanceRecord.Count <= 0)
            {
                MessageBox.Show("该时间段内没有要查找的数据！");
                return;
            }

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
            this.ucInpatientDayBalanceReportNew2.SetFarPoint(this.lstTitle);

            //显示发票汇总数据
            SetInvoiceReprint(this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1);

            // 显示金额数据
            this.SetMoneyValueReprint(this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1);

            // 处理收费清单
            DataTable dtInvoice = null;
            this.inpatientDayBalanceManage.QueryDayBalanceInvoiceDetial(strEmpID, this.lastDate, dayBalanceDate, out dtInvoice);
            this.SetInvoiceDetial(sequence, dtInvoice);

            //SetInvoiceBeginAndEnd(dtInvoice, this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1);

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
            this.ucInpatientDayBalanceReportNew2.SetFarPoint(this.lstTitle);
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

                if (this.alData == null)
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

                FS.FrameWork.Management.PublicTrans.Commit();
                waitForm.Hide();
                MessageBox.Show("日结成功完成");
                PrintInfo(this.neuPanel1);

                if (MessageBox.Show("是否打印收费清单？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
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
            this.panelPrint.BackColor = Color.White;
            this.neuPanel1.BackColor = Color.White;

            // 打印纸张设置
            FS.HISFC.Models.Base.PageSize ps = null;
            ps = psManager.GetPageSize("ZYRJ");

            if (ps == null)
            {
                ps = new FS.HISFC.Models.Base.PageSize("ZYRJ", 787, 550);
                ps.Top = 0;
                ps.Left = 0;

            }

            print.SetPageSize(ps);

            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.PrintPage(ps.Left ,ps.Top , panelPrint);
        }

        #endregion

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
    }
}
