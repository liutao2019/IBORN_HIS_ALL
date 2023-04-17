using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SOC.Fee.DayBalance.Object;
using SOC.Fee.DayBalance.Manager;

namespace SOC.Fee.DayBalance.Outpatient
{
    public partial class ucDayBalanceSumary : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 统计大类业务类
        /// </summary>
        FS.HISFC.BizLogic.Fee.FeeCodeStat feecodeStat = new FS.HISFC.BizLogic.Fee.FeeCodeStat();
        /// <summary>
        /// 打印纸张设置类
        /// </summary>
        FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();

        OutPatientDayBalance opDayBalance = new OutPatientDayBalance();

        #region 属性
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
        /// 报表标题
        /// </summary>
        private string reportTitle = "";

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

        /// <summary>
        /// 统计大类
        /// </summary>
        List<FS.HISFC.Models.Fee.FeeCodeStat> lstFeecodeStat = null;
        #endregion

        public ucDayBalanceSumary()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 清空显示
        /// </summary>
        protected void Clear(string beginDate, string endDate)
        {
            for (int i = 0; i < 12; i++)
            {
                int rowIndex = Convert.ToInt32(i % 3);
                int colIndex = Convert.ToInt32(i / 3) * 2;

                neuSpread1_Sheet1.Cells[rowIndex, colIndex + 1].Text = "";

            }
            neuSpread1_Sheet1.Cells[3, 1].Text = "";
            neuSpread1_Sheet1.Cells[3, 3].Text = "";

            for (int idx = 4; idx < neuSpread1_Sheet1.RowCount; idx++)
            {
                for (int idxj = 0; idxj < neuSpread1_Sheet1.ColumnCount; idxj++)
                {
                    if (neuSpread1_Sheet1.Cells[idx, idxj].Tag != null)
                    {
                        neuSpread1_Sheet1.Cells[idx, idxj].Value = "";
                    }
                }
            }

            //显示报表日结时间和制表员
            string strSpace = "               ";
            string strInfo = "制表员：" + feecodeStat.Operator.Name + strSpace + "日结时间段：" + beginDate + " --- " + endDate;
            this.lblReportInfo.Text = strInfo;
        }

        private void ucDayBalanceSumary_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(strStatClass))
            {
                lstFeecodeStat = this.feecodeStat.QueryFeeStatNameByReportCode(strStatClass);
            }

            this.lbltitle.Text = this.feecodeStat.Hospital.Name + reportTitle;

            this.Clear("", "");

            SetDetailName();

            SetFarPoint(lstTitle);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            int iRes = 0;

            string startDate = this.dtpStart.Value.Date.ToString("yyyy-MM-dd 00:00:00");
            string endDate = this.dtpEnd.Value.Date.ToString("yyyy-MM-dd 23:59:59");

            DataTable dtDetial = null;
            iRes = opDayBalance.QueryDayBalanceSumary(startDate, endDate, out dtDetial);

            if (iRes <= 0)
            {
                MessageBox.Show(opDayBalance.Err);
                return -1;
            }

            this.Clear(startDate, endDate);

            SetDetial(dtDetial);

            DataTable dtPay = null;
            DataTable dtPub = null;

            iRes = opDayBalance.QueryDayBalanceSumaryPubPay(startDate, endDate, out dtPay, out dtPub);
            if (iRes <= 0)
            {
                MessageBox.Show(opDayBalance.Err);
                return -1;
            }

            this.SetPayTypeValue(neuSpread1_Sheet1, dtPay, dtPub);

            return iRes;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
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
            print.PrintPage(0, 0, pnlSumary);

            return 1;
        }


        /// <summary>
        /// 设置显示项目数据
        /// </summary>
        /// <param name="table"></param>
        protected virtual void SetDetial(DataTable table)
        {
            if (table.Rows.Count == 0) return;

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

                decTemp += FS.FrameWork.Function.NConvert.ToDecimal(neuSpread1_Sheet1.Cells[rowIndex, colIndex + 1].Text);

                neuSpread1_Sheet1.Cells[rowIndex, colIndex + 1].Text = decTemp.ToString("0.00");

            }
            neuSpread1_Sheet1.Cells[3, 1].Text = countMoney.ToString("0.00");
            neuSpread1_Sheet1.Cells[3, 3].Text = FS.FrameWork.Public.String.LowerMoneyToUpper(countMoney);

        }

        /// <summary>
        /// 按支付类型显示金额
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="dtPayMode">支付方式汇总</param>
        /// <param name="dtPactEco">减免汇总</param>
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

        #region 方法

        protected void SetDetailName()
        {
            if (lstFeecodeStat != null && lstFeecodeStat.Count > 0)
            {
                int sortID = 0;
                FS.HISFC.Models.Fee.FeeCodeStat feecodeStat = null;
                for (int i = 0; i < lstFeecodeStat.Count; i++)
                {
                    feecodeStat = lstFeecodeStat[i];
                    sortID = feecodeStat.SortID - 1;

                    int rowIndex = Convert.ToInt32(sortID % 3);
                    int colIndex = Convert.ToInt32(sortID / 3) * 2;

                    this.neuSpread1_Sheet1.Cells.Get(rowIndex, colIndex).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    this.neuSpread1_Sheet1.Cells.Get(rowIndex, colIndex).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    this.neuSpread1_Sheet1.Cells.Get(rowIndex, colIndex).Value = feecodeStat.FeeStat.Name;

                    this.neuSpread1_Sheet1.Cells.Get(rowIndex, colIndex + 1).Value = "";
                    this.neuSpread1_Sheet1.Cells.Get(rowIndex, colIndex + 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    this.neuSpread1_Sheet1.Cells.Get(rowIndex, colIndex + 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

                }
            }
            else
            {
                this.neuSpread1_Sheet1.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(0, 0).Value = "西药费";
                this.neuSpread1_Sheet1.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.neuSpread1_Sheet1.Cells.Get(0, 1).Value = "0.00";
                this.neuSpread1_Sheet1.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(0, 2).Value = "诊查费";
                this.neuSpread1_Sheet1.Cells.Get(0, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(0, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.neuSpread1_Sheet1.Cells.Get(0, 3).Value = "0.00";
                this.neuSpread1_Sheet1.Cells.Get(0, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(0, 4).Value = "手术费";
                this.neuSpread1_Sheet1.Cells.Get(0, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.neuSpread1_Sheet1.Cells.Get(0, 5).Value = "0.00";
                this.neuSpread1_Sheet1.Cells.Get(0, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(0, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(0, 6).Value = "床位费";
                this.neuSpread1_Sheet1.Cells.Get(0, 6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(0, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.neuSpread1_Sheet1.Cells.Get(0, 7).ParseFormatString = "n";
                this.neuSpread1_Sheet1.Cells.Get(0, 7).Value = "0.00";
                this.neuSpread1_Sheet1.Cells.Get(0, 7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(1, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(1, 0).Value = "中成药";
                this.neuSpread1_Sheet1.Cells.Get(1, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(1, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.neuSpread1_Sheet1.Cells.Get(1, 1).Value = "0.00";
                this.neuSpread1_Sheet1.Cells.Get(1, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(1, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(1, 2).Value = "化验费";
                this.neuSpread1_Sheet1.Cells.Get(1, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(1, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.neuSpread1_Sheet1.Cells.Get(1, 3).Value = "0.00";
                this.neuSpread1_Sheet1.Cells.Get(1, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(1, 4).Value = "治疗费";
                this.neuSpread1_Sheet1.Cells.Get(1, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.neuSpread1_Sheet1.Cells.Get(1, 5).Value = "0.00";
                this.neuSpread1_Sheet1.Cells.Get(1, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(1, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(1, 6).Value = "护理费";
                this.neuSpread1_Sheet1.Cells.Get(1, 6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(1, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.neuSpread1_Sheet1.Cells.Get(1, 7).Value = "0.00";
                this.neuSpread1_Sheet1.Cells.Get(1, 7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(2, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(2, 0).Value = "中草药";
                this.neuSpread1_Sheet1.Cells.Get(2, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(2, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.neuSpread1_Sheet1.Cells.Get(2, 1).Value = "0.00";
                this.neuSpread1_Sheet1.Cells.Get(2, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(2, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(2, 2).Value = "检查费";
                this.neuSpread1_Sheet1.Cells.Get(2, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(2, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.neuSpread1_Sheet1.Cells.Get(2, 3).Value = "0.00";
                this.neuSpread1_Sheet1.Cells.Get(2, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(2, 4).Value = "材料费";
                this.neuSpread1_Sheet1.Cells.Get(2, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.neuSpread1_Sheet1.Cells.Get(2, 5).Value = "0.00";
                this.neuSpread1_Sheet1.Cells.Get(2, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(2, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(2, 6).Value = "其他费";
                this.neuSpread1_Sheet1.Cells.Get(2, 6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells.Get(2, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.neuSpread1_Sheet1.Cells.Get(2, 7).Value = "0.00";
                this.neuSpread1_Sheet1.Cells.Get(2, 7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }

            this.neuSpread1_Sheet1.Cells.Get(3, 0).Value = "合计：";
            this.neuSpread1_Sheet1.Cells.Get(3, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(3, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(3, 1).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(3, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(3, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(3, 2).TabStop = true;
            this.neuSpread1_Sheet1.Cells.Get(3, 2).Value = "大写:";
            this.neuSpread1_Sheet1.Cells.Get(3, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(3, 3).ColumnSpan = 5;
            this.neuSpread1_Sheet1.Cells.Get(3, 3).Value = "";
        }

        /// <summary>
        /// 设置FarPoint显示
        /// </summary>
        /// <param name="sheet"></param>
        protected void SetFarPoint(List<string> lstTitle)
        {
            FarPoint.Win.Spread.SheetView sheet = this.neuSpread1_Sheet1;

            if (lstTitle == null)
            {
                lstTitle = new List<string>();
            }
            SetFpStyle(sheet, lstTitle);
        }

        /// <summary>
        /// 设置显示格式
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="lstTitle"></param>
        protected void SetFpStyle(FarPoint.Win.Spread.SheetView sheet, List<string> lstTitle)
        {
            try
            {
                if (sheet.Rows.Count > 4)
                {
                    sheet.Rows.Count = 4;
                }

                sheet.RowCount += 1;

                int iCurrentRow = sheet.RowCount - 1;
                int colCount = 4;
                int iRowCount = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(lstTitle.Count * 1.0 / colCount).ToString());

                sheet.RowCount += iRowCount - 1;

                int idx = 0;
                for (int iRow = 0; iRow < iRowCount; iRow++)
                {
                    for (int iCol = 0; iCol < colCount; iCol++)
                    {
                        if (idx >= lstTitle.Count)
                        {
                            break;
                        }

                        sheet.Cells[iCurrentRow + iRow, iCol * 2].Text = lstTitle[idx];
                        sheet.Cells[iCurrentRow + iRow, iCol * 2 + 1].Text = "";
                        sheet.Cells[iCurrentRow + iRow, iCol * 2 + 1].Tag = lstTitle[idx];

                        idx++;
                    }
                }


                #region 制表人等
                sheet.RowCount += 1;
                sheet.Cells[sheet.RowCount - 1, 0].Text = "汇总人：";
                sheet.Cells[sheet.RowCount - 1, 3].Text = "核收人：";

                #endregion

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
    }
}
