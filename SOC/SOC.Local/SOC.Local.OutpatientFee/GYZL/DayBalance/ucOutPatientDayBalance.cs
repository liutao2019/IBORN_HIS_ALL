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

namespace FS.SOC.Local.OutpatientFee.GYZL.DayBalance
{
    /// <summary>
    /// 广医肿瘤门诊发票日结
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
        /// 日结操作时间
        /// </summary>
        string operateDate = "";

        /// <summary>
        /// 日结方法类
        /// </summary>
        FS.SOC.Local.OutpatientFee.GYZL.Functions.OutPatientDayBalance clinicDayBalance = new FS.SOC.Local.OutpatientFee.GYZL.Functions.OutPatientDayBalance();
        // 门诊收费业务层
        FS.HISFC.BizLogic.Fee.Outpatient outpatient = new FS.HISFC.BizLogic.Fee.Outpatient();

        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        /// <summary>
        /// 统计大类业务类
        /// </summary>
        FS.HISFC.BizLogic.Fee.FeeCodeStat feecodeStat = new FS.HISFC.BizLogic.Fee.FeeCodeStat();
        /// <summary>
        /// 打印纸张设置类
        /// </summary>
        FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();
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

        #region 新增变量属性

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
        [Description("报表标题"), Category("设置")]
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
        /// <summary>
        /// 设置日结统计项目，及统计方式
        /// </summary>
        [Category("设置"), Description("设置日结统计项目，及统计方式")]
        public string StrSetting
        {
            get { return strSetting; }
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
                catch (Exception objEx)
                {
                    MessageBox.Show(objEx.Message);
                }

            }

        }

        string strSetting;

        List<string> lstTitle = null;
        List<string> lstStaticType = null;
        List<string> lstPayMode = null;
        List<List<string>> lstPact = null;
        List<decimal> lstValue = null;
        #endregion


        /// <summary>
        /// 全院日结还是单人日结，全院日结不分收费员日结
        /// </summary>
        private string isAll = "1";//‘0’表示全院日结，‘1’表示单个收费员日结
        /// <summary>
        /// 日结人
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

        /// <summary>
        /// 显示票据信息
        /// </summary>
        protected bool blnShowBillInfo = false;
        /// <summary>
        /// 显示使用票据信息
        /// </summary>
        protected bool blnShowUsedBill = false;
        /// <summary>
        /// 显示作废票据信息
        /// </summary>
        protected bool blnShowValiBill = false;
        /// <summary>
        /// 是否显示票据信息
        /// </summary>
        [Category("设置"), Description("是否显示票据信息")]
        public bool BlnShowBillInfo
        {
            get
            {
                return blnShowBillInfo;
            }
            set
            {
                blnShowBillInfo = value;
            }
        }
        /// <summary>
        /// 是否显示使用票据信息
        /// </summary>
        [Category("设置"), Description("是否显示使用票据信息")]
        public bool BlnShowUsedBill
        {
            get
            {
                return blnShowUsedBill;
            }
            set
            {
                blnShowUsedBill = value;
            }
        }
        /// <summary>
        /// 是否显示票据信息
        /// </summary>
        [Category("设置"), Description("是否显示票据信息")]
        public bool BlnShowValiBill
        {
            get
            {
                return blnShowValiBill;
            }
            set
            {
                blnShowValiBill = value;
            }
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
        /// 统计大类帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper StatHelper = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// 标题行
        /// </summary>
        int HeadRows = 0;
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
                if (isAll == "1")
                {
                    empBalance = this.clinicDayBalance.Operator as FS.HISFC.Models.Base.Employee;
                }
                else
                {
                    if (empBalance == null)
                    {
                        empBalance = new FS.HISFC.Models.Base.Employee();
                    }

                    empBalance.ID = "T00001";
                    empBalance.Name = "T-全院";
                }
                // 返回值
                int intReturn = 0;

                // 获取最近一次日结时间
                intReturn = this.GetLastBalanceDate();
                //本次日结开始时间为上次结束时间+1
                this.lastDate = FrameWork.Function.NConvert.ToDateTime(this.lastDate).AddSeconds(1).ToString();
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

                List<FS.HISFC.Models.Fee.FeeCodeStat> lstFeecodeStat = null;
                if (!string.IsNullOrEmpty(strStatClass))
                {
                    lstFeecodeStat = this.feecodeStat.QueryFeeStatNameByReportCode(strStatClass);
                }
                this.ucClinicDayBalanceReportNew1.lstFeecodeStat = lstFeecodeStat;
                this.ucClinicDayBalanceReportNew2.lstFeecodeStat = lstFeecodeStat;


                // 初始化子控件的变量
                this.ucClinicDayBalanceDateControl1.dtLastBalance = NConvert.ToDateTime(this.lastDate);
                this.ucClinicDayBalanceReportNew1.InitUC(clinicDayBalance.Hospital.Name);
                this.ucClinicDayBalanceReportNew2.InitUC(clinicDayBalance.Hospital.Name);

                this.ucClinicDayBalanceReportNew1.Clear();
                this.ucClinicDayBalanceReportNew2.Clear();
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

                intReturn = outpatient.GetLastBalanceDate(this.empBalance, ref lastDate);
                //if (this.isAll == "0")
                //{
                //    intReturn = outpatient.GetLastBalanceDate(this.empBalance, ref lastDate);
                //}
                //else
                //{
                //    intReturn = outpatient.GetLastBalanceDate(this.currentOperator, ref lastDate);
                //}

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
            //显示报表信息
            this.ucClinicDayBalanceReportNew1.Clear(lastDate, dayBalanceDate);

            //获取日结算数据
            ds = new DataSet();

            // {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
            string strEmpID = this.empBalance.ID;
            if (this.isAll == "0")
            {
                strEmpID = "ALL";
            }
            clinicDayBalance.GetDayBalanceDataMZRJ(strEmpID, this.lastDate, dayBalanceDate, ref ds);


            //this.SetDetial(ds.Tables[0]);

            //设置farpoint格式
            //this.ucClinicDayBalanceReportNew1.SetFarPoint(lstTitle);

            //显示发票汇总数据
            //SetInvoice(this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1);

            //显示金额数据
            //this.SetMoneyValue(this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1);

            //显示发票汇总数据
            SetInvoice(this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1);

            this.SetDetial(ds.Tables[0], this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1);
        }

        #endregion

        #region 设置要日结Farpoint数据

        /// <summary>
        /// 设置显示项目数据
        /// </summary>
        /// <param name="table"></param>
        protected virtual void SetDetial(DataTable table,FarPoint.Win.Spread.SheetView sheet)
        {
            if (table.Rows.Count == 0) return;
            //日结明细显示和发票显示一致。
            sheet.Rows.Count += 1;
            //DataView dv = table.DefaultView.ToTable(true, new string[]{"invo_code","invo_name"}).DefaultView;
            DataSet dsFeeStatName = new DataSet ();
            clinicDayBalance.GetDayFeeStatName(ref dsFeeStatName);
            ArrayList FeeStat = new ArrayList();
            if (dsFeeStatName != null&&dsFeeStatName.Tables.Count!=0 && dsFeeStatName.Tables[0].Rows.Count >0)
            {
                FS.FrameWork.Models.NeuObject obj = null;
                foreach (DataRow rows in dsFeeStatName.Tables[0].Rows)
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = rows["fee_stat_cate"].ToString();
                    obj.Name=rows["fee_stat_name"].ToString();
                    FeeStat.Add(obj);
                }
            }
            StatHelper.ArrayObject = FeeStat;
            DataView dv = dsFeeStatName.Tables[0].DefaultView;
            //dv.Sort = "invo_code";
            //dv.RowFilter = "invo_name not in ('西药费','中成费','中草费')";
            dv.RowFilter = "fee_stat_name not in ('西药费','中成费','中草费')";
            if (dv == null || dv.Count <= 0) return;
            
            if (dv.Count + 7 > sheet.ColumnCount)
            {
                sheet.ColumnCount = dv.Count + 3 + 4;
                sheet.ColumnHeaderAutoText = FarPoint.Win.Spread.HeaderAutoText.Blank;
            }
            //------------------head
            sheet.Rows.Count += 2;
            
            
            sheet.Rows.Count += 1;
            sheet.Cells[sheet.RowCount - 2, 0].RowSpan = 2;
            sheet.Cells[sheet.RowCount - 2, 0].Text = "科室";
            sheet.Cells[sheet.RowCount - 2, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 2, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            int startHejiRowIndex = sheet.Rows.Count + 1;

            sheet.Cells[sheet.RowCount - 2, 1].ColumnSpan = 4;
            sheet.Cells[sheet.RowCount - 2, 1].Text = "药品收入";
            sheet.Cells[sheet.RowCount - 2, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 2, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Cells[sheet.RowCount - 1, 1].Text = "西药费";
            sheet.Cells[sheet.RowCount - 1, 1].Tag = "西药费";
            sheet.Cells[sheet.RowCount - 1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 1, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Cells[sheet.RowCount - 1, 2].Text = "中成费";
            sheet.Cells[sheet.RowCount - 1, 2].Tag = "中成费";
            sheet.Cells[sheet.RowCount - 1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 1, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Cells[sheet.RowCount - 1, 3].Text = "中草费";
            sheet.Cells[sheet.RowCount - 1, 3].Tag = "中草费";
            sheet.Cells[sheet.RowCount - 1, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 1, 3].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Cells[sheet.RowCount - 1, 4].Text = "合计";
            sheet.Cells[sheet.RowCount - 1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 1, 4].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Cells[sheet.RowCount - 2, 5].ColumnSpan = dv.Count + 1;
            sheet.Cells[sheet.RowCount - 2, 5].Text = "医疗收入";
            sheet.Cells[sheet.RowCount - 2, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 2, 5].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            for (int i = 0; i < dv.Count; i++)
            {
                sheet.Cells[sheet.RowCount - 1, 5 + i].Text = dv[i]["fee_stat_name"].ToString();
                sheet.Cells[sheet.RowCount - 1, 5 + i].Tag = dv[i]["fee_stat_name"].ToString();
                sheet.Cells[sheet.RowCount - 1, 5 + i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                sheet.Cells[sheet.RowCount - 1, 5 + i].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }

            sheet.Cells[sheet.RowCount - 1, 5 + dv.Count].Text = "合计";
            sheet.Cells[sheet.RowCount - 1, 5 + dv.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 1, 5 + dv.Count].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Cells[sheet.RowCount - 2, 6 + dv.Count].Text = "金额合计";
            sheet.Cells[sheet.RowCount - 2, 6 + dv.Count].RowSpan = 2;
            sheet.Cells[sheet.RowCount - 2, 6 + dv.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 2, 6 + dv.Count].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.HeadRows = sheet.RowCount - 1;//标题行chengym
            //---------------------head

            //-----------data start
            DataView dvData = table.DefaultView;
            string dataRowFilter = "fee_stat_name in ('西药费','中成费','中草费') and  dept_name = {0}";
            Hashtable hs = new Hashtable();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string strDptCode = table.Rows[i]["dept_code"].ToString();
                string strDptName = table.Rows[i]["dept_name"].ToString();
                string strFeeStat = table.Rows[i]["fee_stat_name"].ToString();
                decimal strCost = FS.FrameWork.Function.NConvert.ToDecimal(table.Rows[i][4].ToString());
         
                if (hs.Contains(strDptName))
                {
                    FarPoint.Win.Spread.Cell cell = sheet.GetCellFromTag(null, strFeeStat);
                    sheet.Cells[sheet.RowCount - 1, cell.Column.Index].Text =  strCost.ToString("0.00");
                }
                else
                {
                    sheet.Rows.Count += 1;
                    hs.Add(strDptName,"");
                    sheet.Cells[sheet.RowCount - 1, 0].Text = strDptName;
                    sheet.Cells[sheet.RowCount - 1, 0].Tag = strDptCode;
                    sheet.Cells[sheet.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    sheet.Cells[sheet.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    FarPoint.Win.Spread.Cell cell = sheet.GetCellFromTag(null, strFeeStat);
                    sheet.Cells[sheet.RowCount - 1, cell.Column.Index].Text =  strCost.ToString("0.00");            
                }
            }
            //----------data end
            FarPoint.Win.Spread.CellType.NumberCellType cellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            //填充0
            for (int i = startHejiRowIndex - 1; i < sheet.RowCount; i++)
            {    
                for (int j = 1; j < sheet.ColumnCount; j++)
                {
                    sheet.Cells[i, j].CellType = cellType;
                    if (sheet.Cells[i, j].Text == null || sheet.Cells[i, j].Text == "" || sheet.Cells[i, j].Text == "0") 
                    {
                        sheet.Cells[i, j].Text = "0.00";
                    }
                }
            }

            //药品收入合计，医疗收入合计
            for (int i = startHejiRowIndex-1; i < sheet.Rows.Count; i++)
            {
                 sheet.Cells[i, 4].Formula = string.Format("sum(B{0}:D{0})",(i+1).ToString());

                 sheet.Cells[i, 5 + dv.Count].Formula = string.Format("sum(F{0}:{1}{0})", (i + 1).ToString(), (char)(69 + dv.Count));

                 sheet.Cells[i, 6 + dv.Count].Formula = string.Format("sum(E{0},{1}{0})", (i + 1).ToString(), (char)(70 + dv.Count));
            }

            //行合计
            sheet.Rows.Count += 1;
            sheet.Cells[sheet.RowCount - 1, 0].Text = "合计:";
            sheet.Cells[sheet.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;  
            for(int i= 1;i< dv.Count + 7;i++)
            {
                string strFormula = "sum(" + (char)(65 + i) + startHejiRowIndex.ToString() + ":"
                                           + (char)(65 + i) + (sheet.RowCount - 1).ToString() 
                                    + ")";
                sheet.Cells[sheet.RowCount - 1, i].CellType = cellType;
                sheet.Cells[sheet.RowCount - 1, i].Formula = strFormula;
                sheet.Cells[sheet.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                sheet.Cells[sheet.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }
        }


        /// <summary>
        /// 设置显示已日结项目数据
        /// </summary>
        /// <param name="table"></param>
        protected virtual void SetRePrintDetial(DataTable table, FarPoint.Win.Spread.SheetView sheet)
        {
            if (table.Rows.Count == 0) return;
            //日结明细显示和发票显示一致。
            sheet.Rows.Count += 1;
            //DataView dv = table.DefaultView.ToTable(true, new string[]{"invo_code","invo_name"}).DefaultView;
            DataSet dsFeeStatName = new DataSet();
            clinicDayBalance.GetDayFeeStatName(ref dsFeeStatName);
            DataView dv = dsFeeStatName.Tables[0].DefaultView;
            //dv.Sort = "invo_code";
            //dv.RowFilter = "invo_name not in ('西药费','中成费','中草费')";
            dv.RowFilter = "fee_stat_name not in ('西药费','中成费','中草费')";
            if (dv == null || dv.Count <= 0) return;

            if (dv.Count + 7 > sheet.ColumnCount)
            {
                sheet.ColumnCount = dv.Count + 3 + 4;
                sheet.ColumnHeaderAutoText = FarPoint.Win.Spread.HeaderAutoText.Blank;
            }
            //------------------head
            sheet.Rows.Count += 2;

            sheet.Rows.Count += 1;
            sheet.Cells[sheet.RowCount - 2, 0].RowSpan = 2;
            sheet.Cells[sheet.RowCount - 2, 0].Text = "科室";
            sheet.Cells[sheet.RowCount - 2, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 2, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            int startHejiRowIndex = sheet.Rows.Count + 1;

            sheet.Cells[sheet.RowCount - 2, 1].ColumnSpan = 4;
            sheet.Cells[sheet.RowCount - 2, 1].Text = "药品收入";
            sheet.Cells[sheet.RowCount - 2, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 2, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Cells[sheet.RowCount - 1, 1].Text = "西药费";
            sheet.Cells[sheet.RowCount - 1, 1].Tag = "西药费";
            sheet.Cells[sheet.RowCount - 1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 1, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Cells[sheet.RowCount - 1, 2].Text = "中成费";
            sheet.Cells[sheet.RowCount - 1, 2].Tag = "中成费";
            sheet.Cells[sheet.RowCount - 1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 1, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Cells[sheet.RowCount - 1, 3].Text = "中草费";
            sheet.Cells[sheet.RowCount - 1, 3].Tag = "中草费";
            sheet.Cells[sheet.RowCount - 1, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 1, 3].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Cells[sheet.RowCount - 1, 4].Text = "合计";
            sheet.Cells[sheet.RowCount - 1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 1, 4].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Cells[sheet.RowCount - 2, 5].ColumnSpan = dv.Count + 1;
            sheet.Cells[sheet.RowCount - 2, 5].Text = "医疗收入";
            sheet.Cells[sheet.RowCount - 2, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 2, 5].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            for (int i = 0; i < dv.Count; i++)
            {
                sheet.Cells[sheet.RowCount - 1, 5 + i].Text = dv[i]["fee_stat_name"].ToString();
                sheet.Cells[sheet.RowCount - 1, 5 + i].Tag = dv[i]["fee_stat_name"].ToString();
                sheet.Cells[sheet.RowCount - 1, 5 + i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                sheet.Cells[sheet.RowCount - 1, 5 + i].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }

            sheet.Cells[sheet.RowCount - 1, 5 + dv.Count].Text = "合计";
            sheet.Cells[sheet.RowCount - 1, 5 + dv.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 1, 5 + dv.Count].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Cells[sheet.RowCount - 2, 6 + dv.Count].Text = "金额合计";
            sheet.Cells[sheet.RowCount - 2, 6 + dv.Count].RowSpan = 2;
            sheet.Cells[sheet.RowCount - 2, 6 + dv.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 2, 6 + dv.Count].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            //---------------------head

            //-----------data start
            DataView dvData = table.DefaultView;
            string dataRowFilter = "fee_stat_name in ('西药费','中成费','中草费') and  dept_name = {0}";
            Hashtable hs = new Hashtable();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string strDptName = table.Rows[i]["dept_name"].ToString();
                string strFeeStat = table.Rows[i]["fee_stat_name"].ToString();
                decimal strCost = FS.FrameWork.Function.NConvert.ToDecimal(table.Rows[i][3].ToString());

                if (hs.Contains(strDptName))
                {
                    FarPoint.Win.Spread.Cell cell = sheet.GetCellFromTag(null, strFeeStat);
                    sheet.Cells[sheet.RowCount - 1, cell.Column.Index].Text = strCost.ToString("0.00");
                }
                else
                {
                    sheet.Rows.Count += 1;
                    hs.Add(strDptName, "");
                    sheet.Cells[sheet.RowCount - 1, 0].Text = strDptName;
                    sheet.Cells[sheet.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    sheet.Cells[sheet.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    FarPoint.Win.Spread.Cell cell = sheet.GetCellFromTag(null, strFeeStat);
                    sheet.Cells[sheet.RowCount - 1, cell.Column.Index].Text = strCost.ToString("0.00");
                }
            }
            //----------data end
            FarPoint.Win.Spread.CellType.NumberCellType cellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            //填充0
            for (int i = startHejiRowIndex - 1; i < sheet.RowCount; i++)
            {
                for (int j = 1; j < sheet.ColumnCount; j++)
                {
                    sheet.Cells[i, j].CellType = cellType;
                    if (sheet.Cells[i, j].Text == null || sheet.Cells[i, j].Text == "" || sheet.Cells[i, j].Text == "0")
                    {
                        sheet.Cells[i, j].Text = "0.00";
                    }
                }
            }
            //药品收入合计，医疗收入合计
            for (int i = startHejiRowIndex - 1; i < sheet.Rows.Count; i++)
            {
                sheet.Cells[i, 4].Formula = string.Format("sum(B{0}:D{0})", (i + 1).ToString());

                sheet.Cells[i, 5 + dv.Count].Formula = string.Format("sum(F{0}:{1}{0})", (i + 1).ToString(), (char)(69 + dv.Count));

                sheet.Cells[i, 6 + dv.Count].Formula = string.Format("sum(E{0},{1}{0})", (i + 1).ToString(), (char)(70 + dv.Count));
            }

            //行合计
            sheet.Rows.Count += 1;
            sheet.Cells[sheet.RowCount - 1, 0].Text = "合计:";
            sheet.Cells[sheet.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            for (int i = 1; i < dv.Count + 7; i++)
            {
                string strFormula = "sum(" + (char)(65 + i) + startHejiRowIndex.ToString() + ":"
                                           + (char)(65 + i) + (sheet.RowCount - 1).ToString()
                                    + ")";
                sheet.Cells[sheet.RowCount - 1, i].Formula = strFormula;
                sheet.Cells[sheet.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                sheet.Cells[sheet.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }
        }

        /// <summary>
        /// 添加日结实体到列表
        /// </summary>
        private void AddDayBalanceToArray()
        {
            dayBalance.BeginTime = NConvert.ToDateTime(this.lastDate);
            dayBalance.EndTime = NConvert.ToDateTime(this.dayBalanceDate);

            dayBalance.Oper.ID = this.empBalance.ID;
            dayBalance.Oper.Name = this.empBalance.Name;

            // {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
            //if (this.isAll == "0")
            //{
            //    dayBalance.Oper.ID = this.empBalance.ID;
            //    dayBalance.Oper.Name = this.empBalance.Name;
            //}
            //else
            //{
            //    dayBalance.Oper.ID = currentOperator.ID;
            //    dayBalance.Oper.Name = currentOperator.Name;
            //}
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
            string strEmpID = this.empBalance.ID;
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

            int totInvoiceCount = dv.Count;
            //有效发票数
            dv.RowFilter = "cancel_flag='1'";//"trans_type = '1'";
            int totValidInvoiceCount = dv.Count;

           //退费、作废票据号 
            dv.RowFilter = "cancel_flag in('0','2','3') and trans_type='2'";
            int totUnValidInvoiceCount = dv.Count;
            string InvoiceStr = GetInvoiceStr(dv);

            //有效发票的起止票据号
            dv.RowFilter = "cancel_flag='1'";
            string invoiceStartAndEnd = GetInvoiceStartAndEnd(dv);
            string[] sTemp = invoiceStartAndEnd.Split('，');
            sheet.RowCount = sTemp.Length;
           

            DataSet ds1 = new DataSet();
            int resultValue1 = clinicDayBalance.GetDayInvoiceDataGYZL(strEmpID, this.lastDate, dayBalanceDate, ref ds1);
            if (resultValue1 == -1)
            {
                MessageBox.Show(clinicDayBalance.Err);
                return;
            }
            DataTable table1 = ds1.Tables[0];
            if (table1.Rows.Count == 0)
            {
                return;
            }
            DataView dv1 = table1.DefaultView;
            //string strRowFilter = "print_invoiceno in ({0})";
            for (int i = 0; i < sTemp.Length; i++)
            {
                dv1.RowFilter = string.Format("print_invoiceno in ({0})", GetRowFilterInvoiceno(sTemp[i]));
                if (dv1 != null && dv1.Count > 0)
                {
                    sheet.Cells[i, 0].Text = sTemp[i];
                    sheet.Cells[i, 1].Text = GetPrintInvoicenoCount(sTemp[i]).ToString();
                    sheet.Cells[i, 2].Text = dv1.ToTable().Compute("sum(记账)", "").ToString();//记账
                    sheet.Cells[i, 3].Text = dv1.ToTable().Compute("sum(中行磁卡)", "").ToString();//中行磁卡
                    sheet.Cells[i, 4].Text = dv1.ToTable().Compute("sum(商行磁卡)", "").ToString();//商行磁卡
                    sheet.Cells[i, 5].Text = dv1.ToTable().Compute("sum(现金)", "").ToString();//现金
                    sheet.Cells[i, 6].Text = dv1.ToTable().Compute("sum(汇款)", "").ToString();//汇款
                    sheet.Cells[i, 7].Text = dv1.ToTable().Compute("sum(支票)", "").ToString();//支票
                    sheet.Cells[i, 8].Text = dv1.ToTable().Compute("sum(退款)", "").ToString();//退款
                    sheet.Cells[i, 9].Text = dv1.ToTable().Compute("sum(合计)", "").ToString();//合计
                }
            }

            sheet.Rows.Count += 1;
            dv1.RowFilter = ""; 
            sheet.Cells[sheet.RowCount - 1, 0].Text = "总计(发票数)";
            sheet.Cells[sheet.RowCount - 1, 1].Text = totValidInvoiceCount.ToString();
            sheet.Cells[sheet.RowCount - 1, 2].Text = dv1.Table.Compute("sum(记账)", "").ToString();//记账
            sheet.Cells[sheet.RowCount - 1, 3].Text = dv1.Table.Compute("sum(中行磁卡)", "").ToString();//中行磁卡
            sheet.Cells[sheet.RowCount - 1, 4].Text = dv1.Table.Compute("sum(商行磁卡)", "").ToString();//商行磁卡
            sheet.Cells[sheet.RowCount - 1, 5].Text = dv1.Table.Compute("sum(现金)", "").ToString();//现金
            sheet.Cells[sheet.RowCount - 1, 6].Text = dv1.Table.Compute("sum(汇款)", "").ToString();//汇款
            sheet.Cells[sheet.RowCount - 1, 7].Text = dv1.Table.Compute("sum(支票)", "").ToString();//支票
            sheet.Cells[sheet.RowCount - 1, 8].Text = dv1.Table.Compute("sum(退款)", "").ToString();//退款
            sheet.Cells[sheet.RowCount - 1, 9].Text = dv1.Table.Compute("sum(合计)", "").ToString();//合计

            FarPoint.Win.Spread.CellType.NumberCellType cellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            //填充0
            for (int i = 0; i < sheet.RowCount; i++)
            {
                for (int j = 2; j < sheet.ColumnCount; j++)
                {
                    sheet.Cells[i, j].CellType = cellType;
                    if (sheet.Cells[i, j].Text == null || sheet.Cells[i, j].Text == ""
                        || sheet.Cells[i, j].Text == "0")
                    {
                        sheet.Cells[i, j].Text = "0.00";
                    }
                }
            }


            sheet.Rows.Count += 1;
            sheet.Cells[sheet.RowCount - 1, 0].ColumnSpan = 4;
            //string strMsg = "共有发票" + totInvoiceCount.ToString() + "张，其中有效发票"
            //    + totValidInvoiceCount.ToString() + "张，作废发票有" + totUnValidInvoiceCount.ToString()+ "张";
            //作废的存在同样的发票号不同的记录 所以应该计算总张数；
            string strMsg = "共有发票" + (totValidInvoiceCount + totUnValidInvoiceCount).ToString() + "张，其中有效发票"
                + totValidInvoiceCount.ToString() + "张，作废发票有" + totUnValidInvoiceCount.ToString() + "张";
            sheet.Cells[sheet.RowCount - 1, 0].Text = strMsg;

            sheet.Rows.Count += 1;
            sheet.Cells[sheet.RowCount - 1, 0].ColumnSpan = 10;
            sheet.Cells[sheet.RowCount - 1, 0].Text = "作废发票号码为：" + InvoiceStr;

            return;
        }

        /// <summary>
        /// 查询并显示发票数据 -- 
        /// {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
        /// </summary>
        /// <param name="table"></param>
        protected virtual void SetInvoiceReprint(FarPoint.Win.Spread.SheetView sheet)
        {
            //获取发票数据
            DataSet ds = new DataSet();
            string strEmpID = this.empBalance.ID;
            if (this.isAll == "0")
            {
                strEmpID = "ALL";
            }
            int resultValue = clinicDayBalance.GetDayInvoiceDataNewReprint(strEmpID, this.lastDate, dayBalanceDate, ref ds);
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
            int totInvoiceCount = dv.Count;
            //有效发票数
            dv.RowFilter = "cancel_flag='1'";//"trans_type = '1'";
            int totValidInvoiceCount = dv.Count;

            //退费、作废票据号 
            dv.RowFilter = "cancel_flag in('0','2','3') and trans_type='2'";
            int totUnValidInvoiceCount = dv.Count;
            string InvoiceStr = GetInvoiceStr(dv);

            //有效发票的起止票据号
            dv.RowFilter = "cancel_flag='1'";
            string invoiceStartAndEnd = GetInvoiceStartAndEnd(dv);
            string[] sTemp = invoiceStartAndEnd.Split('，');
            sheet.RowCount = sTemp.Length;


            DataSet ds1 = new DataSet();
            int resultValue1 = clinicDayBalance.GetDayInvoiceYJDataGYZL(strEmpID, this.lastDate, dayBalanceDate, ref ds1);
            if (resultValue1 == -1)
            {
                MessageBox.Show(clinicDayBalance.Err);
                return;
            }
            DataTable table1 = ds1.Tables[0];
            if (table1.Rows.Count == 0)
            {
                return;
            }
            DataView dv1 = table1.DefaultView;
            //string strRowFilter = "print_invoiceno in ({0})";
            for (int i = 0; i < sTemp.Length; i++)
            {
                dv1.RowFilter = string.Format("print_invoiceno in ({0})", GetRowFilterInvoiceno(sTemp[i]));
                if (dv1 != null && dv1.Count > 0)
                {
                    sheet.Cells[i, 0].Text = sTemp[i];
                    sheet.Cells[i, 1].Text = GetPrintInvoicenoCount(sTemp[i]).ToString();
                    sheet.Cells[i, 2].Text = dv1.ToTable().Compute("sum(合计)", "").ToString();//合计
                    sheet.Cells[i, 3].Text = dv1.ToTable().Compute("sum(记账)", "").ToString();//记账
                    sheet.Cells[i, 4].Text = dv1.ToTable().Compute("sum(中行磁卡)", "").ToString();//中行磁卡
                    sheet.Cells[i, 5].Text = dv1.ToTable().Compute("sum(商行磁卡)", "").ToString();//商行磁卡
                    sheet.Cells[i, 6].Text = dv1.ToTable().Compute("sum(现金)", "").ToString();//现金
                    sheet.Cells[i, 7].Text = dv1.ToTable().Compute("sum(汇款)", "").ToString();//汇款
                    sheet.Cells[i, 8].Text = dv1.ToTable().Compute("sum(支票)", "").ToString();//支票
                    sheet.Cells[i, 9].Text = dv1.ToTable().Compute("sum(退款)", "").ToString();//退款
                }
            }

            sheet.Rows.Count += 1;
            dv1.RowFilter = "";
            sheet.Cells[sheet.RowCount - 1, 0].Text = "总计(发票数)";
            sheet.Cells[sheet.RowCount - 1, 1].Text = totValidInvoiceCount.ToString();
            sheet.Cells[sheet.RowCount - 1, 2].Text = dv1.Table.Compute("sum(合计)", "").ToString();//合计
            sheet.Cells[sheet.RowCount - 1, 3].Text = dv1.Table.Compute("sum(记账)", "").ToString();//记账
            sheet.Cells[sheet.RowCount - 1, 4].Text = dv1.Table.Compute("sum(中行磁卡)", "").ToString();//中行磁卡
            sheet.Cells[sheet.RowCount - 1, 5].Text = dv1.Table.Compute("sum(商行磁卡)", "").ToString();//商行磁卡
            sheet.Cells[sheet.RowCount - 1, 6].Text = dv1.Table.Compute("sum(现金)", "").ToString();//现金
            sheet.Cells[sheet.RowCount - 1, 7].Text = dv1.Table.Compute("sum(汇款)", "").ToString();//汇款
            sheet.Cells[sheet.RowCount - 1, 8].Text = dv1.Table.Compute("sum(支票)", "").ToString();//支票
            sheet.Cells[sheet.RowCount - 1, 9].Text = dv1.Table.Compute("sum(退款)", "").ToString();//退款

            FarPoint.Win.Spread.CellType.NumberCellType cellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            //填充0
            for (int i = 0; i < sheet.RowCount; i++)
            {
                for (int j = 2; j < sheet.ColumnCount; j++)
                {
                    sheet.Cells[i, j].CellType = cellType;
                    if (sheet.Cells[i, j].Text == null || sheet.Cells[i, j].Text == ""
                        || sheet.Cells[i, j].Text == "0")
                    {
                        sheet.Cells[i, j].Text = "0.00";
                    }
                }
            }

            sheet.Rows.Count += 1;
            sheet.Cells[sheet.RowCount - 1, 0].ColumnSpan = 4;
            string strMsg = "共有发票" + totInvoiceCount.ToString() + "张，其中有效发票"
                + totValidInvoiceCount.ToString() + "张，作废发票有" + totUnValidInvoiceCount.ToString() + "张";
            sheet.Cells[sheet.RowCount - 1, 0].Text = strMsg;

            sheet.Rows.Count += 1;
            sheet.Cells[sheet.RowCount - 1, 0].ColumnSpan = 10;
            sheet.Cells[sheet.RowCount - 1, 0].Text = "作废发票号码为：" + InvoiceStr;

            return;
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

        #region 获得起始、终止票据号

        private string GetInvoiceStartAndEnd(DataView dv)
        {
            if (dv != null && dv.Count != 0)
            {
                StringBuilder sb = new StringBuilder();
                int count = dv.Count - 1;
                string minStr = GetPrintInvoiceno(dv[0]);
                string maxStr = GetPrintInvoiceno(dv[0]);
                for (int i = 0; i < count ; i++)
                    for (int j = i + 1; j <= count; j++)
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
                                sb.Append(minStr + "～" + maxStr + "，");
                            }
                            minStr = GetPrintInvoiceno(dv[j]);
                            break;
                        }
                        else
                        {
                            break;
                        }

                    }
                maxStr = GetPrintInvoiceno(dv[count]);
                if (minStr == maxStr)
                {
                    sb.Append(maxStr);
                }
                else
                {
                    sb.Append(minStr + "～" + maxStr);
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

        private int GetPrintInvoicenoCount(string invoicenoStartEnd)
        {
            int count = 1;
            if (invoicenoStartEnd.Contains("～"))
            {
                string[] invoiceStartEnd = invoicenoStartEnd.Split('～');
                long startInt = Convert.ToInt64(invoiceStartEnd[0].TrimStart(new char[] { '0' }).PadLeft(12, '0'));
                long endInt = Convert.ToInt64(invoiceStartEnd[1].TrimStart(new char[] { '0' }).PadLeft(12, '0'));
                count = int.Parse((endInt - startInt+1).ToString());
            }
            return count;
        }


        private string GetRowFilterInvoiceno(string invoicenoStartEnd)
        {
            StringBuilder sb = new StringBuilder();
            if (invoicenoStartEnd.Contains("～"))
            {
                string[] invoiceStartEnd = invoicenoStartEnd.Split('～');
                long startInt = Convert.ToInt64(invoiceStartEnd[0].TrimStart(new char[] { '0' }).PadLeft(12, '0'));
                long endInt = Convert.ToInt64(invoiceStartEnd[1].TrimStart(new char[] { '0' }).PadLeft(12, '0'));
                int count = int.Parse((endInt - startInt).ToString());
                for (int i = 0; i <= count; i++)
                {
                    sb.Append("'");
                    sb.Append(startInt.ToString().PadLeft(12,'0'));
                    sb.Append("'");
                    if(i!=count)
                    sb.Append(",");
                    startInt = startInt + 1;
                }
            }
            else
            {
                sb.Append("'");
                sb.Append(invoicenoStartEnd.ToString().PadLeft(12,'0'));
                sb.Append("'");
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
            string employeeID = this.empBalance.ID;
            if (this.isAll == "0")
            {
                employeeID = "ALL";
            }

            // 执行非常慢，屏蔽 

            resultValue = clinicDayBalance.GetDayBalanceCancelMoney(employeeID, this.lastDate, dayBalanceDate, ref money);
            if (resultValue != -1)
            {
                money1 += money;
            }
            //作废金额
            resultValue = clinicDayBalance.GetDayBalanceFalseMoney(employeeID, this.lastDate, dayBalanceDate, ref money);
            if (resultValue != -1)
            {
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
        /// 设置显示金额数据
        /// {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
        /// </summary>
        protected virtual void SetMoneyValueReprint(FarPoint.Win.Spread.SheetView sheet)
        {
            decimal money = 0;
            int resultValue;
            int resultValue1;


            #region 作废金额

            decimal money1 = 0;

            //退费金额
            // {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
            string employeeID = this.empBalance.ID;
            if (this.isAll == "0")
            {
                employeeID = "ALL";
            }

            // 执行非常慢，屏蔽 

            resultValue = clinicDayBalance.GetDayBalanceCancelMoneyReprint(employeeID, this.lastDate, dayBalanceDate, ref money);
            if (resultValue != -1)
            {
                money1 += money;
            }
            //作废金额
            resultValue = clinicDayBalance.GetDayBalanceFalseMoneyReprint(employeeID, this.lastDate, dayBalanceDate, ref money);
            if (resultValue != -1)
            {
                money1 += money;
            }

            SetOneCellText(sheet, EnumCellName.A004, money1.ToString());

            #endregion

            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();

            //支付方式金额
            resultValue = clinicDayBalance.GetDayBalancePayTypeMoneyReprint(employeeID, this.lastDate, dayBalanceDate, ref ds);
            if (resultValue == -1)
            {
                MessageBox.Show(clinicDayBalance.Err);
                return;
            }


            resultValue = clinicDayBalance.QueryDayBalancePactPubMoneyReprint(employeeID, this.lastDate, dayBalanceDate, ref ds2);
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
        }

        /// <summary>
        /// 显示公费金额数据
        /// </summary>
        /// <param name="sheet">SheetView</param>
        /// <param name="ds">DataSet</param>
        protected virtual void SetPublicValue(FarPoint.Win.Spread.SheetView sheet, DataSet ds)
        {
        }

        //{0EA3CF1A-2F03-46c3-83AE-1543B00BBDDB}
        /// <summary>
        /// 显示减免金额数据
        /// </summary>
        /// <param name="sheet">SheetView</param>
        /// <param name="ds">DataSet</param>
        protected virtual void SetRebateValue(FarPoint.Win.Spread.SheetView sheet, DataSet ds)
        {
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

            intReturn = this.clinicDayBalance.GetBalanceRecord(this.empBalance, dtFrom, dtTo, ref balanceRecord);
            //if (this.isAll == "0")
            //{
            //    intReturn = this.clinicDayBalance.GetBalanceRecord(this.empBalance, dtFrom, dtTo, ref balanceRecord);
            //}
            //else
            //{
            //    intReturn = this.clinicDayBalance.GetBalanceRecord(this.currentOperator, dtFrom, dtTo, ref balanceRecord);
            //}
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
            string strEmpID = this.empBalance.ID;
            if (this.isAll == "0")
            {
                strEmpID = "ALL";
            }
            //日结重打走另外的方法
            //clinicDayBalance.GetDayBalanceDataMZRJ(strEmpID, this.lastDate, this.dayBalanceDate, ref ds);
            clinicDayBalance.GetDayBalanceDataMZRJReprint(strEmpID, this.lastDate, this.dayBalanceDate, ref ds);

            //this.SetRePrintDetial(ds.Tables[0]);

            //设置farpoint格式
            //this.ucClinicDayBalanceReportNew2.SetFarPoint(lstTitle);

            //显示发票汇总数据
            //日结重打走另外的方法
            //SetInvoice(this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1);
            //SetInvoiceReprint(this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1);

            //显示金额数据
            //this.SetMoneyValue(this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1);
            //this.SetMoneyValueReprint(this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1);

            //显示发票汇总数据
            SetInvoiceReprint(this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1);

            this.SetRePrintDetial(ds.Tables[0],this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1);

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
            this.ucClinicDayBalanceReportNew2.SetFarPoint(lstTitle);
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
            #region 判断该起始时间是否已经日结了

            string dtLastTemp = "";
            int returnValue = this.outpatient.GetLastBalanceDate(this.empBalance, ref dtLastTemp);
            if (returnValue == -1)
            {
                MessageBox.Show("获取上次日结时间失败！不能进行日结操作！");
                return;
            }
            else if (returnValue == 0)
            {
                //没有日结过，可以让继续日结，啥也不做；
            }
            else
            {
                //日结过
                if (!this.lastDate.Equals(FS.FrameWork.Function.NConvert.ToDateTime(dtLastTemp).AddSeconds(1).ToString()))
                {
                    MessageBox.Show("已经做过日结，请退出界面重新进！");
                    return;
                }
            }

            #endregion 

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

                string strEmpID = this.empBalance.ID;
                string strOperID = this.empBalance.ID;
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

                //this.ucClinicDayBalanceDateControl1.dtpBalanceDate.Value = this.ucClinicDayBalanceDateControl1.dtLastBalance;
                //this.QueryDayBalanceData();
            }
        }
        #endregion

        #region 事件
        private void ucOutPatientDayBalance_Load(object sender, EventArgs e)
        {
            this.InitUC();
            this.ucClinicDayBalanceReportNew1.enentFarpiontClick += new ucOutPatientDayBalanceReport.FarpiontClick(ucClinicDayBalanceReportNew1_enentFarpiontClick);
        }
        /// <summary>
        /// 双击查明细事件
        /// </summary>
        void ucClinicDayBalanceReportNew1_enentFarpiontClick()
        {
            FarPoint.Win.Spread.SheetView sheet = this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1;
            if (sheet.Cells[sheet.ActiveRowIndex, 0].Tag == null)
            {
                MessageBox.Show("未获取到第一列的科室编码，请联系信息科！");
                return;
            }
            string deptCode = sheet.Cells[sheet.ActiveRowIndex, 0].Tag.ToString();//科室编码
            string dtBegin = this.lastDate;
            string dtEnd = this.dayBalanceDate;
            string StatCode = StatHelper.GetID(sheet.Cells[this.HeadRows,sheet.ActiveColumnIndex].Text);
            string strEmpID = this.empBalance.ID;
            DataSet ds = new DataSet();

            clinicDayBalance.GetDayBalanceDataMZRJDetail(strEmpID, dtBegin,dtEnd,deptCode,StatCode ,ref ds);
            this.ucClinicDayBalanceReportNew1.neuSpread1.ActiveSheetIndex = 1;
            FarPoint.Win.Spread.SheetView sheet1 = this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet2;
            sheet1.DataSource = ds;
            sheet1.Rows.Add(sheet1.RowCount, 1);
            sheet1.Cells[sheet1.RowCount - 1, 0].Text = "合计：";
            sheet1.Cells[sheet1.RowCount - 1, 7].Formula = "SUM(H1:" + "H" + (sheet1.RowCount - 1).ToString() + ")";
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
            toolBarService.AddToolButton("取消日结", "取消最近一次日结信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.H合并取消, true, false, null);

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

        #region 取消最近一次日结
        /// <summary>
        /// 取消最近一次日结
        /// </summary>
        public void UnDoDayBalance()
        {
            string strOperID = this.empBalance.ID;
            FS.FrameWork.Models.NeuObject balance = null;

            int iRes = this.clinicDayBalance.QueryLastBalanceRecord(strOperID, out balance);
            if (iRes <= 0)
            {
                MessageBox.Show(this.clinicDayBalance.Err);
                return;
            }

            if (balance.User02 == "1")
            {
                MessageBox.Show("最近一次日结信息已审核，不允许取消！");

                return;
            }

            iRes = this.clinicDayBalance.UnDoOperDayBalance(balance.ID, balance.Name, balance.Memo);

            if (iRes <= 0)
            {
                MessageBox.Show("操作失败！" + this.clinicDayBalance.Err);
            }
            else
            {
                MessageBox.Show("操作成功！");

                alData.Clear();
                this.InitUC();
            }
        }

        #endregion

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

            // 打印纸张设置
            FS.HISFC.Models.Base.PageSize ps = null;
            ps = psManager.GetPageSize("MZRJ");

            if (ps == null)
            {
                ps = new FS.HISFC.Models.Base.PageSize("MZRJ", 787, 550);
                ps.Top = 0;
                ps.Left = 0;

            }

            print.SetPageSize(ps);

            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.PrintPage(0, 0, panelPrint);

        }

        public override int Export(object sender, object neuObject)
        {
            this.ExportInfo();
            return base.Export(sender, neuObject);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        private void ExportInfo()
        {

            bool tr = false;
            string fileName = "";
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "excel|*.xls";
            saveFile.Title = "导出到Excel";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(saveFile.FileName))
                {
                    fileName = saveFile.FileName;
                    tr = this.ucClinicDayBalanceReportNew1.neuSpread1.SaveExcel(fileName, FarPoint.Excel.ExcelSaveFlags.SaveCustomColumnHeaders, new FarPoint.Excel.ExcelWarningList());
                }
                else
                {
                    MessageBox.Show("文件名字不能为空!");
                    return;
                }

                if (tr)
                {
                    MessageBox.Show("导出成功!");
                }
                else
                {
                    MessageBox.Show("导出失败!");
                }

            }
        }

        #endregion

    }
}
