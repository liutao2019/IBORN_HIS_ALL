using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SOC.Fee.Report.OutpatientReport.GYZL
{
    public partial class ucOutPatientDayBalanceQuitFee : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucOutPatientDayBalanceQuitFee()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 日结方法类
        /// </summary>
        FS.SOC.Local.OutpatientFee.GYZL.Functions.OutPatientDayBalance clinicDayBalance = new FS.SOC.Local.OutpatientFee.GYZL.Functions.OutPatientDayBalance();
        FS.HISFC.BizLogic.Fee.FeeReport feeMgr = new FS.HISFC.BizLogic.Fee.FeeReport();
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();

        //根据统计大类代码获取的统计大类名称
        DataSet dsFeeStat = new DataSet();

        FS.FrameWork.Public.ObjectHelper StatHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 每页打印行数
        /// </summary>
        private int lineCount = 32;

        /// <summary>
        /// 每页打印行数
        /// </summary>
        [Description("每页打印行数"), Category("打印设置")]
        public int LineCount
        {
            get
            {
                return this.lineCount;
            }
            set
            {
                this.lineCount = value;
            }
        }

        /// <summary>
        /// 报表标题
        /// </summary>
        private string titleName = string.Empty;

        /// <summary>
        /// 报表标题
        /// </summary>
        [Description("报表标题"),Category("打印设置")]
        public string TitleName
        {
            set
            {
                this.titleName = value;
            }
            get
            {
                return this.titleName;
            }
        }

        /// <summary>
        /// 是否横着打印
        /// </summary>
        private bool isLandScape = true; 

        /// <summary>
        /// 是否横着打印
        /// </summary>
        /// <returns></returns>
        [Description("是否横着打印"), Category("打印设置")]
        public bool IsLandScape
        {
            set
            {
                this.isLandScape = value;
            }
            get
            {
                return this.isLandScape;
            }
        }

        /// <summary>
        /// sqlid
        /// </summary>
        private string sqlID1 = string.Empty;

        [Description("报表1SQLID"), Category("数据")]
        public string SqlID1
        {
            get
            {
                return this.sqlID1;
            }
            set
            {
                this.sqlID1 = value;
            }
        }

        /// <summary>
        /// sqlid
        /// </summary>
        private string sqlID2 = string.Empty;

        [Description("报表2SQLID"), Category("数据")]
        public string SqlID2
        {
            get
            {
                return this.sqlID2;
            }
            set
            {
                this.sqlID2 = value;
            }
        }

        /// <summary>
        /// 报表类型
        /// </summary>
        private string reportType = string.Empty;

        /// <summary>
        /// 报表类型
        /// </summary>
        [Description("报表类型"), Category("数据")]
        public string ReportType
        {
            get
            {
                return this.reportType;
            }

            set
            {
                this.reportType = value;
            }
        }

        string strStatClass = "MZ01";//默认设置为门诊发票统计大类
        /// <summary>
        /// 显示统计大类
        /// </summary>
        [Category("设置统计大类代码，如：MZ01"), Description("显示统计大类")]
        public string StatClass
        {
            get { return strStatClass; }
            set { strStatClass = value; }
        }


        /// <summary>
        /// 报表是否包含员工条件
        /// </summary>
        [Category("报表设置"), Description("报表是否包含员工条件")]
        public bool BlnEmployeeCondition
        {
            get { return pnlEmployee.Visible; }
            set { pnlEmployee.Visible = value; }
        }

        string strPriv = "0";
        /// <summary>
        /// 报表权限设置，0 = 无限制；1=科室级别；2=员工级别
        /// </summary>
        [Category("报表设置"), Description("报表权限设置，0 = 无限制；1=科室级别；2=员工级别")]
        public string StrPriv
        {
            get { return strPriv; }
            set { strPriv = value; }
        }

        private string filePath = string.Empty;
        /// <summary>
        /// 明细表头统计大类行
        /// </summary>
        private int SecondReportBegin = 0;
        #endregion

        #region  方法

        public int QueryReport()
        {
            //清空,必须要清空(因为有些行是特定设置的。如果直接将DataSource的值变了，但是neuspread_sheet已有的行的格式是不变，所以一般情况下要将sheet清空)
            this.neuSpread1_Sheet1.Rows.Count = 0;

            string begin = this.beginDate.Value.ToString();
            string end = this.endDate.Value.ToString();

            string strOperID = "";
            if (pnlEmployee.Visible && this.cmbFeeOper.SelectedItem != null)
            {
                strOperID = this.cmbFeeOper.SelectedItem.ID;
            }   

            #region 按发票起始明细查询作废先，需求改了
            /*//-----收费员和发票信息-----start
            DataSet dsOperInvoice = new DataSet();
            if (this.feeMgr.QueryItemIncomeByOper("GYZL.Report.QueryBalanceInvoiceByOper.Select", begin, end, ref dsOperInvoice) == -1)
            {
                MessageBox.Show(this.feeMgr.Err);
                return -1;
            }
            DataTable tableOperInvoice = dsOperInvoice.Tables[0];
            if (tableOperInvoice.Rows.Count <= 0)
            {
                MessageBox.Show("该段时间内没有数据!");
                return -1;
            }

            DataView dvOperInvoice = tableOperInvoice.DefaultView;

            int totInvoiceCount = dvOperInvoice.Count;
            //有效发票数
            dvOperInvoice.RowFilter = "cancel_flag='1'";//"trans_type = '1'";
            int totValidInvoiceCount = dvOperInvoice.Count;


            //有效发票的起止票据号
            dvOperInvoice.RowFilter = "cancel_flag='1'";
            string invoiceStartAndEnd = GetInvoiceStartAndEnd(dvOperInvoice);
            string[] sTemp = invoiceStartAndEnd.Split('，');
            neuSpread1_Sheet1.RowCount = sTemp.Length;

            //-----收费员和发票信息-----end


            DataSet dsResult = new DataSet();

            if (this.feeMgr.QueryItemIncomeByOper(this.sqlID, begin, end, ref dsResult) == -1)
            {
                MessageBox.Show(this.feeMgr.Err);
                return -1;
            }

            if (dsResult.Tables[0].Rows.Count <= 0)
            {
                MessageBox.Show("该段时间内没有数据!");
                return -1;
            }
            DataTable table = dsResult.Tables[0];
            DataView dv1 = table.DefaultView;
            //string strRowFilter = "print_invoiceno in ({0})";
            FarPoint.Win.Spread.CellType.NumberCellType cellType = new FarPoint.Win.Spread.CellType.NumberCellType();

            FarPoint.Win.Spread.CellType.TextCellType txtTppe = new FarPoint.Win.Spread.CellType.TextCellType();

            for (int i = 0; i < sTemp.Length; i++)
            {
                dv1.RowFilter = string.Format("print_invoiceno in ({0})", GetRowFilterInvoiceno(sTemp[i]));
                if (dv1 != null && dv1.Count > 0)
                {

                    neuSpread1_Sheet1.Cells[i, 0].CellType = txtTppe;
                    neuSpread1_Sheet1.Cells[i, 1].CellType = txtTppe;
                    neuSpread1_Sheet1.Cells[i, 2].CellType = txtTppe;
                    neuSpread1_Sheet1.Cells[i, 3].CellType = cellType;
                    neuSpread1_Sheet1.Cells[i, 4].CellType = cellType;
                    neuSpread1_Sheet1.Cells[i, 5].CellType = cellType;
                    neuSpread1_Sheet1.Cells[i, 6].CellType = cellType;
                    neuSpread1_Sheet1.Cells[i, 7].CellType = cellType;
                    neuSpread1_Sheet1.Cells[i, 8].CellType = cellType;
                    neuSpread1_Sheet1.Cells[i, 9].CellType = cellType;
                    neuSpread1_Sheet1.Cells[i, 10].CellType = cellType;

                    neuSpread1_Sheet1.Cells[i, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    neuSpread1_Sheet1.Cells[i, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    neuSpread1_Sheet1.Cells[i, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                    neuSpread1_Sheet1.Cells[i, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    neuSpread1_Sheet1.Cells[i, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    neuSpread1_Sheet1.Cells[i, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                    neuSpread1_Sheet1.Cells[i, 0].Text = dv1[0]["empl_name"].ToString();
                    neuSpread1_Sheet1.Cells[i, 1].Text = sTemp[i];
                    neuSpread1_Sheet1.Cells[i, 2].Text = GetPrintInvoicenoCount(sTemp[i]).ToString();
                    neuSpread1_Sheet1.Cells[i, 3].Text = dv1.ToTable().Compute("sum(合计)", "").ToString();//合计
                    neuSpread1_Sheet1.Cells[i, 4].Text = dv1.ToTable().Compute("sum(记账)", "").ToString();//记账
                    neuSpread1_Sheet1.Cells[i, 5].Text = dv1.ToTable().Compute("sum(中行磁卡)", "").ToString();//中行磁卡
                    neuSpread1_Sheet1.Cells[i, 6].Text = dv1.ToTable().Compute("sum(商行磁卡)", "").ToString();//商行磁卡
                    neuSpread1_Sheet1.Cells[i, 7].Text = dv1.ToTable().Compute("sum(现金)", "").ToString();//现金
                    neuSpread1_Sheet1.Cells[i, 8].Text = dv1.ToTable().Compute("sum(汇款)", "").ToString();//汇款
                    neuSpread1_Sheet1.Cells[i, 9].Text = dv1.ToTable().Compute("sum(支票)", "").ToString();//支票
                    neuSpread1_Sheet1.Cells[i, 10].Text = dv1.ToTable().Compute("sum(优惠)", "").ToString();//优惠
                }
            }

            //退费、作废票据号 
            dvOperInvoice.RowFilter = "cancel_flag in('0','2','3') and trans_type='2'";
            int totUnValidInvoiceCount = dvOperInvoice.Count;
            string InvoiceStr = GetInvoiceStr(dvOperInvoice);
            string[] sTemp1 = InvoiceStr.Split('|');

            //连续处理
            ////string invoiceStartAndEnd1 = GetInvoiceStartAndEnd(dvOperInvoice);
            ////string[] sTemp1 = invoiceStartAndEnd1.Split('，');

            for (int i = 0; i < sTemp1.Length; i++)
            {
                //trans_type = '2'必须加，不然sql文中sum了
                dv1.RowFilter = string.Format("print_invoiceno in ({0})", GetRowFilterInvoiceno(sTemp1[i]));
                if (dv1 != null && dv1.Count > 0)
                {
                    //退费的正负都显示，不然合计值不对。
                    for (int j = 0; j < dv1.Count; j++)
                    {
                        neuSpread1_Sheet1.RowCount += 1;
                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].CellType = txtTppe;
                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 1].CellType = txtTppe;
                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 2].CellType = txtTppe;
                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 3].CellType = cellType;
                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 4].CellType = cellType;
                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 5].CellType = cellType;
                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 6].CellType = cellType;
                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 7].CellType = cellType;
                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 8].CellType = cellType;
                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 9].CellType = cellType;
                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 10].CellType = cellType;

                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                        //neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].Text = dv1[j]["empl_name"].ToString() + "(退费)";
                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 1].Text = sTemp1[i];
                        if (j == 1)
                        {
                            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].Text = dv1[j]["empl_name"].ToString() + "(退费反)";
                            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 2].Text = "0";
                        }
                        else
                        {
                            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].Text = dv1[j]["empl_name"].ToString() + "(退费正)";
                            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 2].Text = GetPrintInvoicenoCount(sTemp1[i]).ToString();
                        }

                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 3].Text = dv1[j]["合计"].ToString();//合计
                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 4].Text = dv1[j]["记账"].ToString();//记账
                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 5].Text = dv1[j]["中行磁卡"].ToString();//中行磁卡
                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 6].Text = dv1[j]["商行磁卡"].ToString();//商行磁卡
                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 7].Text = dv1[j]["现金"].ToString();//现金
                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 8].Text = dv1[j]["汇款"].ToString();//汇款
                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 9].Text = dv1[j]["支票"].ToString();//支票
                        neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 10].Text = dv1[j]["优惠"].ToString();//优惠
                    }
                }
            }
            */
            #endregion 按发票起始明细查询作废先，需求改了

            DataSet dsResult1 = new DataSet();
            if (clinicDayBalance.GetDataSetBySqlID(this.sqlID1, strOperID, begin, end,"", ref dsResult1) == -1)
            {
                MessageBox.Show(this.feeMgr.Err);
                return -1;
            }

            if (dsResult1.Tables[0].Rows.Count <= 0)
            {
                MessageBox.Show("该段时间内没有数据!");
                return -1;
            }

            DataSet dsResult2 = new DataSet();
            if (clinicDayBalance.GetDataSetBySqlID(this.sqlID2, strOperID, begin, end, this.StatClass, ref dsResult2) == -1)
            {
                MessageBox.Show(this.feeMgr.Err);
                return -1;
            }

            if (dsResult2.Tables[0].Rows.Count <= 0)
            {
                MessageBox.Show("该段时间内没有数据!");
                return -1;
            }
            

            //this.neuSpread1_Sheet1.DataSource = dsResult.Tables[0];
            //this.neuSpread1_Sheet1.DataAutoSizeColumns = false;
            //for (int j = 0; j < this.neuSpread1_Sheet1.Rows.Count; j++)
            //{
                //this.neuSpread1_Sheet1.Columns[j].Width = this.neuSpread1_Sheet1.Columns[j].GetPreferredWidth();
            //}

            #region  设置表头位置

            this.lblTitle.Text = this.titleName;

            //this.lblInfo.Text = "打印人：" + this.feeMgr.Operator.Name + "     时间范围：" + this.beginDate.Value.ToString() + " 至 " + this.endDate.Value.ToString();
            this.lblInfo.Text = "统计时间：" + this.beginDate.Value.ToShortDateString() + " － " + this.endDate.Value.ToShortDateString() + "     收费员：" + this.cmbFeeOper.Text.Trim();

            //设置标题位置
            decimal spreadWidt = 0m;
            foreach (FarPoint.Win.Spread.Column fpColumn in this.neuSpread1_Sheet1.Columns)
            {
                spreadWidt += (decimal)fpColumn.Width;
            }

            if (spreadWidt > this.neuPrint.Width)
            {
                spreadWidt = this.neuPrint.Width;
            }

            spreadWidt = spreadWidt - this.lblTitle.Width;
            int titleX = FS.FrameWork.Function.NConvert.ToInt32((spreadWidt / 2));
            if (titleX <= 0)
            {
                titleX = 1;
            }

            this.lblTitle.Location = new Point(titleX, this.lblTitle.Location.Y);

            #endregion

            if (this.dsFeeStat == null || this.dsFeeStat.Tables[0].Rows.Count <= 0) return -1;

            this.SetReport1ColumnHeader();
            this.ShowData(dsResult1);
            this.SetReport2ColumnHeader(this.dsFeeStat.Tables[0]);
            int autoColCount = dsFeeStat.Tables[0].Rows.Count - 3;
            SetReport2Data(dsResult2.Tables[0], autoColCount);
            //this.AddSumRow();
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            return 1;
        }


        /// <summary>
        /// 显示数据
        /// </summary>
        /// <param name="dsResult"></param>
        public void ShowData(DataSet dsResult)
        {
            DataTable dt = dsResult.Tables[0];
            int index = 0;

            foreach (DataRow dr in dt.Rows)
            {
                this.neuSpread1_Sheet1.Rows.Add(index, 1);

                FarPoint.Win.Spread.CellType.NumberCellType numberCellType = new FarPoint.Win.Spread.CellType.NumberCellType();

                FarPoint.Win.Spread.CellType.TextCellType txtTppe = new FarPoint.Win.Spread.CellType.TextCellType();
                this.neuSpread1_Sheet1.Cells[index, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[index, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[index, 0].CellType = txtTppe;
                this.neuSpread1_Sheet1.Cells[index, 0].Text = dr["name"].ToString();

                this.neuSpread1_Sheet1.Cells[index, 1].CellType = txtTppe;
                this.neuSpread1_Sheet1.Cells[index, 1].Text = dr["ori_print_invoiceno"].ToString().PadLeft(12, '0');

                this.neuSpread1_Sheet1.Cells[index, 2].CellType = numberCellType;
                this.neuSpread1_Sheet1.Cells[index, 2].Text = dr["ori_tot_cost"].ToString();

                this.neuSpread1_Sheet1.Cells[index, 3].CellType = numberCellType;
                this.neuSpread1_Sheet1.Cells[index, 3].Text = dr["ori_own_cost"].ToString();

                this.neuSpread1_Sheet1.Cells[index, 4].CellType = numberCellType;
                this.neuSpread1_Sheet1.Cells[index, 4].Text = dr["ori_pub_cost"].ToString();

                this.neuSpread1_Sheet1.Cells[index, 5].CellType = txtTppe;
                this.neuSpread1_Sheet1.Cells[index, 5].Text = dr["now_print_invoiceno"].ToString().PadLeft(12, '0');
                if (this.neuSpread1_Sheet1.Cells[index, 5].Text.Trim() == "000000000000")
                {
                    this.neuSpread1_Sheet1.Cells[index, 5].Text = "";
                }

                this.neuSpread1_Sheet1.Cells[index, 6].CellType = numberCellType;
                this.neuSpread1_Sheet1.Cells[index, 6].Text = dr["now_tot_cost"].ToString();

                this.neuSpread1_Sheet1.Cells[index, 7].CellType = numberCellType;
                this.neuSpread1_Sheet1.Cells[index, 7].Text = dr["now_own_cost"].ToString();

                this.neuSpread1_Sheet1.Cells[index, 8].CellType = numberCellType;
                this.neuSpread1_Sheet1.Cells[index, 8].Text = dr["now_pub_cost"].ToString();

                this.neuSpread1_Sheet1.Cells[index, 9].CellType = numberCellType;
                this.neuSpread1_Sheet1.Cells[index, 9].Text = dr["quit_tot_cost"].ToString();

                this.neuSpread1_Sheet1.Cells[index, 10].CellType = numberCellType;
                this.neuSpread1_Sheet1.Cells[index, 10].Text = dr["quit_own_cost"].ToString();

                this.neuSpread1_Sheet1.Cells[index, 11].CellType = numberCellType;
                this.neuSpread1_Sheet1.Cells[index, 11].Text = dr["quit_pub_cost"].ToString();

                index++;

            }
            neuSpread1_Sheet1.Rows.Count += 1;
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].Text = "合计:";
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            FarPoint.Win.Spread.CellType.NumberCellType numCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            for (int i = 2; i < neuSpread1_Sheet1.ColumnCount; i++)
            {
                if (i == 5) continue;

                string strFormula = "sum(" + (char)(65 + i) + "1:"
                                           + (char)(65 + i) + (neuSpread1_Sheet1.RowCount - 1).ToString()
                                    + ")";
                neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, i].Formula = strFormula;
                neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, i].CellType = numCellType;
                neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }
        }

        private void SetReport2Data(DataTable table, int autoColCount)
        {
            int startHejiRowIndex = neuSpread1_Sheet1.Rows.Count + 1;
            //-----------data start
            DataView dvData = table.DefaultView;
            string dataRowFilter = "fee_stat_name in ('西药费','中成费','中草费') and  dept_name = {0}";
            Hashtable hs = new Hashtable();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string strCardno = table.Rows[i]["card_no"].ToString();
                string strName = table.Rows[i]["name"].ToString();
                string strDeptCode = table.Rows[i]["reg_dpcd"].ToString();
                string strDptName = table.Rows[i]["dept_name"].ToString();
                string strFeeStat = table.Rows[i]["fee_stat_name"].ToString();
                decimal strCost = FS.FrameWork.Function.NConvert.ToDecimal(table.Rows[i]["tot_cost"].ToString());

                if (hs.Contains(strCardno + strDptName))
                {
                    FarPoint.Win.Spread.Cell cell = neuSpread1_Sheet1.GetCellFromTag(null, strFeeStat);
                    neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, cell.Column.Index].Text = strCost.ToString("0.00");
                }
                else
                {
                    neuSpread1_Sheet1.Rows.Count += 1;
                    hs.Add(strCardno + strDptName, "");
                    neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].Text = strName;
                    neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].Tag = strCardno;
                    neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                    neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 1].Text = strDptName;
                    neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 1].Tag = strDeptCode;
                    neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                    FarPoint.Win.Spread.Cell cell = neuSpread1_Sheet1.GetCellFromTag(null, strFeeStat);
                    neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, cell.Column.Index].Text = strCost.ToString("0.00");
                }
            }
            //----------data end
            FarPoint.Win.Spread.CellType.NumberCellType cellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            //填充0
            for (int i = startHejiRowIndex - 1; i < neuSpread1_Sheet1.RowCount; i++)
            {
                for (int j = 2; j < neuSpread1_Sheet1.ColumnCount; j++)
                {
                    neuSpread1_Sheet1.Cells[i, j].CellType = cellType;
                    if (neuSpread1_Sheet1.Cells[i, j].Text == null || neuSpread1_Sheet1.Cells[i, j].Text == ""
                        || neuSpread1_Sheet1.Cells[i, j].Text == "0")
                    {
                        neuSpread1_Sheet1.Cells[i, j].Text = "0.00";
                    }
                }
            }

            //药品收入合计，医疗收入合计
            for (int i = startHejiRowIndex - 1; i < neuSpread1_Sheet1.Rows.Count; i++)
            {
                neuSpread1_Sheet1.Cells[i, 5].Formula = string.Format("sum(C{0}:E{0})", (i + 1).ToString());

                neuSpread1_Sheet1.Cells[i, 6 + autoColCount].Formula = string.Format("sum(G{0}:{1}{0})", (i + 1).ToString(), (char)(70 + autoColCount));

                neuSpread1_Sheet1.Cells[i, 7 + autoColCount].Formula = string.Format("sum(F{0},{1}{0})", (i + 1).ToString(), (char)(71 + autoColCount));
            }

            //行合计
            neuSpread1_Sheet1.Rows.Count += 1;
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].Text = "合计:";
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            for (int i = 2; i < neuSpread1_Sheet1.ColumnCount; i++)
            {
                string strFormula = "sum(" + (char)(65 + i) + this.SecondReportBegin.ToString()+":"
                                           + (char)(65 + i) + (neuSpread1_Sheet1.RowCount - 1).ToString()
                                    + ")";
                neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, i].Formula = strFormula;
                neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, i].CellType = cellType;
                neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }
        }

        /// <summary>
        /// 增加汇总行
        /// </summary>
        /// <returns></returns>
        public void AddSumRow()
        {
            int index = this.neuSpread1_Sheet1.Rows.Count;
            this.neuSpread1_Sheet1.Rows.Add(index, 1);

            this.neuSpread1_Sheet1.Cells[index, 0].ColumnSpan = 1;
            this.neuSpread1_Sheet1.Cells[index, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[index, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[index, 0].Text = "合计:";
            for (int i = 1; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                decimal totCost = 0m;
                for (int j = 0; j < this.neuSpread1_Sheet1.Rows.Count - 1; j++)
                {
                    totCost += FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[j, i].Text);
                }

                if ((this.reportType == "2") && (i == 1))
                {
                    this.neuSpread1_Sheet1.Cells[index, i].Text = totCost.ToString();
                    this.neuSpread1_Sheet1.Cells[index, i].Locked = true;
                    continue ; 
                }

                FarPoint.Win.Spread.CellType.NumberCellType cellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                this.neuSpread1_Sheet1.Cells[index, i].CellType = cellType;
                this.neuSpread1_Sheet1.Cells[index, i].Text = totCost.ToString();
                this.neuSpread1_Sheet1.Cells[index, i].Locked = true;
            }

            if (this.reportType == "4")
            {
                FarPoint.Win.LineBorder line = new FarPoint.Win.LineBorder(System.Drawing.Color.White, 0, true, true, true, true);
                //增加缴款人和收款人字段
                index = this.neuSpread1_Sheet1.Rows.Count;
                this.neuSpread1_Sheet1.Rows.Add(index, 1);
                this.neuSpread1_Sheet1.Rows[index].Border = line;
                this.neuSpread1_Sheet1.Cells[index, 0].Text = "缴款人：";
                this.neuSpread1_Sheet1.Cells[index, 1].ColumnSpan = 2;

                this.neuSpread1_Sheet1.Cells[index, 3].Text = "收款人：";
                this.neuSpread1_Sheet1.Cells[index, 4].ColumnSpan = 2;
            }
            

        }

        #endregion

        #region 事件

        protected override void OnLoad(EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            DateTime now = feeMgr.GetDateTimeFromSysDateTime();
            this.beginDate.Value = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            this.endDate.Value = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);

            // 当前登录员工
            FS.HISFC.Models.Base.Employee employee = conMgr.Operator as FS.HISFC.Models.Base.Employee;
            // 人员
            ArrayList arlEmployee = this.managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.F);
            ArrayList arlEmployeeList = new ArrayList();

            #region 人员权限设置
            switch (strPriv)
            {
                case "0":
                    FS.HISFC.Models.Base.Employee emp = new FS.HISFC.Models.Base.Employee();
                    emp.ID = "ALL";
                    emp.Name = "全部";
                    arlEmployeeList.Add(emp);

                    arlEmployeeList.AddRange(arlEmployee);
                    break;

                case "1":
                    foreach (FS.HISFC.Models.Base.Employee emp1 in arlEmployee)
                    {
                        if (emp1.Dept.ID == employee.Dept.ID)
                        {
                            arlEmployeeList.Add(emp1);
                        }
                    }

                    break;

                case "2":
                    foreach (FS.HISFC.Models.Base.Employee emp2 in arlEmployee)
                    {
                        if (emp2.ID == employee.ID)
                        {
                            arlEmployeeList.Add(emp2);
                        }
                    }

                    break;

                default:
                    break;
            }
            this.cmbFeeOper.AddItems(arlEmployeeList);
            this.cmbFeeOper.SelectedIndex = 0;
            #endregion

            //设置报表头
            InitSetting();

            this.lblTitle.Text = this.titleName;

            this.filePath = Application.StartupPath + @".\profile\GYZLDayBalanceQuitFee.xml";
            if (System.IO.File.Exists(filePath))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.neuSpread1_Sheet1, this.filePath);
            }
            else
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.filePath);
            }


            base.OnLoad(e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryReport();
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            if (MessageBox.Show("是否打印?", "提示信息", MessageBoxButtons.YesNo,MessageBoxIcon.Information,MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return 1;
            }
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            FS.HISFC.Models.Base.PageSize ps = null;
            FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            ps = pgMgr.GetPageSize("ClinicReport");

            if (ps == null)
            {
                //默认为A4纸张
                ps = new FS.HISFC.Models.Base.PageSize("A4", 827, 1169);
            }
            
            print.SetPageSize(ps);

            int fromPage = 1;
            int toPage = (System.Int32)Math.Ceiling((double)this.neuSpread1_Sheet1.Rows.Count/lineCount);

            for (int i = fromPage; i <= toPage; i++)
            {
                for (int j = 0; j < this.neuSpread1_Sheet1.Rows.Count; j++)
                {
                    if (j >= (i - 1) * this.lineCount && (j + 1) <= i * this.lineCount)
                    {
                        this.neuSpread1_Sheet1.Rows[j].Visible = true;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Rows[j].Visible = false;
                    }
                }

                print.PrintPage(0, 0, this.neuPrint);
            }

            //打印完之后全部显示
            for (int k = 0; k < this.neuSpread1_Sheet1.Rows.Count; k++)
            {
                this.neuSpread1_Sheet1.Rows[k].Visible = true;
            }


            return base.OnPrint(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            this.ExportInfo();
            return base.Export(sender, neuObject);
        }

        #endregion

        private void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.filePath);
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

            try
            {
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(saveFile.FileName))
                    {
                        fileName = saveFile.FileName;
                        //tr = this.neuSpread1.SaveExcel(fileName, FarPoint.Excel.ExcelSaveFlags.SaveCustomColumnHeaders, new FarPoint.Excel.ExcelWarningList());
                        tr = (this.neuSpread1.Export()==1);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void InitSetting()
        {
            if (string.IsNullOrEmpty(this.StatClass))
            {
                MessageBox.Show("请设置统计大类代码!例如门诊发票StatClass设置为MZ01");
                return;
            }
            dsFeeStat = feeMgr.GetFeeStatNameByReportCode(this.StatClass);

            if (dsFeeStat == null || dsFeeStat.Tables[0].Rows.Count <= 0)
            {
                MessageBox.Show("请检查统计大类代码是不是没有维护或者已经作废！");
                return;
            }
            //add chengym 2012-7-25用于获取明细分类信息
            ArrayList statList = new ArrayList();
            FS.FrameWork.Models.NeuObject obj = null;
            foreach (DataRow rows in dsFeeStat.Tables[0].Rows)
            {
                obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = rows["fee_stat_cate"].ToString();
                obj.Name = rows["fee_stat_name"].ToString();
                statList.Add(obj);
            }
            StatHelper.ArrayObject = statList;
            //end add
            this.SetReport1ColumnHeader();
            this.SetReport2ColumnHeader(dsFeeStat.Tables[0]);
        }

        /// <summary>
        /// 根据统计大类数动态设置报表列
        /// </summary>
        /// <param name="dtFeeStatName">统计大类代码和名称数据集</param>
        private void SetReport1ColumnHeader()
        {
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ColumnCount = 12;

            #region 报表头1
            this.neuSpread1_Sheet1.ColumnHeader.RowCount = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].Text = "患者姓名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].ColumnSpan = 4;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].Text = "原发票";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 1].Text = "号码";
            //this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 1].Column.Tag = "号码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 2].Text = "金额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 3].Text = "自费";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 3].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 4].Text = "记账";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 4].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].ColumnSpan = 4;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].Text = "新发票";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 5].Text = "号码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 5].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 6].Text = "金额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 6].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 7].Text = "自费";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 7].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 8].Text = "记账";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 8].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 9].ColumnSpan = 3;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 9].Text = "退费";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 9].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 9].Text = "金额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 9].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 10].Text = "自费";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 10].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 11].Text = "记账";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 11].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            #endregion  报表头1
        }

        /// <summary>
        /// 根据统计大类数动态设置报表列
        /// </summary>
        /// <param name="dtFeeStatName">统计大类代码和名称数据集</param>
        private void SetReport2ColumnHeader(DataTable dtFeeStatName)
        {
            neuSpread1_Sheet1.Rows.Count += 2;//上下报表空2行；

            DataView dv = dtFeeStatName.DefaultView;
            dv.RowFilter = "";
            if (dv == null || dv.Count <= 0) return;
            //重新设置列数
            if (dv.Count + 5 > neuSpread1_Sheet1.ColumnCount)
            {
                neuSpread1_Sheet1.ColumnCount = dv.Count + 5;
                //for (int i = 12; i < dv.Count + 5; i++)
                //{
                //    neuSpread1_Sheet1.ColumnHeader.Cells[0, i].RowSpan = 2;
                //    neuSpread1_Sheet1.ColumnHeader.Cells[0, i].ColumnSpan = dv.Count + 5 - 12;
                //    neuSpread1_Sheet1.ColumnHeader.Cells[0, i].Text = "";
                //}   
                neuSpread1_Sheet1.ColumnHeaderAutoText = FarPoint.Win.Spread.HeaderAutoText.Blank;

            }
            dv.RowFilter = "fee_stat_name not in ('西药费','中成费','中草费')";

            #region 报表头2

            //------------------head
            neuSpread1_Sheet1.Rows.Count += 1;

            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 2, 0].RowSpan = 2;
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 2, 0].Text = "患者姓名";
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 2, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 2, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 2, 1].RowSpan = 2;
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 2, 1].Text = "科室";
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 2, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 2, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 2, 2].ColumnSpan = 4;
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 2, 2].Text = "药品收入";
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 2, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 2, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 2].Text = "西药费";
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 2].Tag = "西药费";
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 3].Text = "中成费";
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 3].Tag = "中成费";
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 3].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 4].Text = "中草费";
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 4].Tag = "中草费";
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 4].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 5].Text = "合计";
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 5].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 2, 6].ColumnSpan = dv.Count + 1;
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 2, 6].Text = "医疗收入";
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 2, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 2, 6].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            for (int i = 0; i < dv.Count; i++)
            {
                neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 6 + i].Text = dv[i]["fee_stat_name"].ToString();
                neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 6 + i].Tag = dv[i]["fee_stat_name"].ToString();
                neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 6 + i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 6 + i].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }

            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 6 + dv.Count].Text = "合计";
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 6 + dv.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 6 + dv.Count].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 2, 7 + dv.Count].Text = "金额合计";
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 2, 7 + dv.Count].RowSpan = 2;
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 2, 7 + dv.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 2, 7 + dv.Count].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.SecondReportBegin = neuSpread1_Sheet1.RowCount - 1;
            #endregion
        }

        #region 发票号起止号处理

        private string GetInvoiceStartAndEnd(DataView dv)
        {
            if (dv != null && dv.Count != 0)
            {
                StringBuilder sb = new StringBuilder();
                int count = dv.Count - 1;
                string minStr = GetPrintInvoiceno(dv[0]);
                string maxStr = GetPrintInvoiceno(dv[0]);
                for (int i = 0; i < count; i++)
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
                count = int.Parse((endInt - startInt + 1).ToString());
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
                    sb.Append(startInt.ToString().PadLeft(12, '0'));
                    sb.Append("'");
                    if (i != count)
                        sb.Append(",");
                    startInt = startInt + 1;
                }
            }
            else
            {
                sb.Append("'");
                sb.Append(invoicenoStartEnd.ToString().PadLeft(12, '0'));
                sb.Append("'");
            }
            return sb.ToString();
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

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (neuSpread1.ActiveSheet == neuSpread1_Sheet1)
            {
                string strOperID = "";
                if (pnlEmployee.Visible && this.cmbFeeOper.SelectedItem != null)
                {
                    strOperID = this.cmbFeeOper.SelectedItem.ID;
                }   
                DateTime dtBegin = this.beginDate.Value;
                DateTime dtEnd = this.endDate.Value;
                if (this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Tag == null)
                {
                    MessageBox.Show("患者门诊号获取错误");
                    return;
                }
                string CardNO = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Tag.ToString().PadLeft(10,'0');
                if (this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 1].Tag == null)
                {
                    MessageBox.Show("患者科室编码获取错误");
                    return;
                }
                string dept_code=this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 1].Tag.ToString();
                string statCode = StatHelper.GetID(this.neuSpread1_Sheet1.Cells[this.SecondReportBegin, this.neuSpread1_Sheet1.ActiveColumnIndex].Text);
                if (statCode == null)
                {
                    MessageBox.Show("不是统计大类列！请重新选择。");
                    return;
                }
                DataSet ds = new DataSet();
                clinicDayBalance.GetDayBalanceDataMZRJDetailFeeBack(strOperID, dtBegin.ToString(), dtEnd.ToString(), CardNO, dept_code, statCode, ref ds);
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                neuSpread1_Sheet2.DataSource = ds;
                neuSpread1.ActiveSheetIndex = 1;
                neuSpread1_Sheet2.Rows.Add(neuSpread1_Sheet2.RowCount, 1);
                neuSpread1_Sheet2.Cells[neuSpread1_Sheet2.RowCount - 1, 0].Text = "合计：";
                neuSpread1_Sheet2.Cells[neuSpread1_Sheet2.RowCount - 1, 7].Formula = "SUM(H1:" + "H" + (neuSpread1_Sheet2.RowCount - 1).ToString() + ")";
            }
        }



    }
}
