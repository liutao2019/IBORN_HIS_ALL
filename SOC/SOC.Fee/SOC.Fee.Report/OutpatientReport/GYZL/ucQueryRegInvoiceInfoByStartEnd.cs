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
    public partial class ucQueryRegInvoiceInfoByStartEnd : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucQueryRegInvoiceInfoByStartEnd()
        {
            InitializeComponent();
        }

        #region 变量

        FS.HISFC.BizLogic.Fee.FeeReport feeMgr = new FS.HISFC.BizLogic.Fee.FeeReport();
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
        DataSet dsFeeStat = new DataSet();

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
        [Description("报表标题"), Category("打印设置")]
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
        private string sqlID = string.Empty;

        [Description("报表SQLID"), Category("数据")]
        public string SqlID
        {
            get
            {
                return this.sqlID;
            }
            set
            {
                this.sqlID = value;
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
        [Category("设置"), Description("显示统计大类")]
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

        /// <summary>
        /// 报表是否包含科室条件
        /// </summary>
        [Category("报表设置"), Description("报表是否包含科室条件")]
        public bool BlnDeptCondition
        {
            get { return pnlDept.Visible; }
            set { pnlDept.Visible = value; }
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

        #endregion

        #region  方法

        public int QueryReport()
        {
            //清空,必须要清空(因为有些行是特定设置的。如果直接将DataSource的值变了，但是neuspread_sheet已有的行的格式是不变，所以一般情况下要将sheet清空)
            this.neuSpread1_Sheet1.Rows.Count = 0;
            this.neuSpread1_Sheet1.ColumnCount = 0;

            //string begin = this.beginDate.Value.ToString();
            //string end = this.endDate.Value.ToString();

            string invoiceStart = this.tbInvoiceStart.Text.Trim();
            string invoiceEnd = this.tbInvoiceEnd.Text.Trim();

            if (string.IsNullOrEmpty(this.tbInvoiceStart.Text.Trim()))
            {
                invoiceStart = "ALL";
            }
            if (string.IsNullOrEmpty(this.tbInvoiceEnd.Text.Trim()))
            {
                invoiceEnd = "ALL";
            }          

            DataSet dsResult = new DataSet();
            string[] param={ invoiceStart, invoiceEnd};
            this.feeMgr.ExecQuery(this.sqlID, ref dsResult, param);
            //if (this.feeMgr.GYZLQueryItemIncomeByDept(this.sqlID, invoiceStart, invoiceEnd, "", "", ref dsResult) == -1)
            //{
            //    MessageBox.Show(this.feeMgr.Err);
            //    return -1;
            //}

            if (dsResult.Tables[0].Rows.Count <= 0)
            {
                MessageBox.Show("该段时间内没有数据!");
                return -1;
            }
            this.neuSpread1_Sheet1.DataSource = dsResult;

            //this.neuSpread1_Sheet1.DataSource = dsResult.Tables[0];
            //this.neuSpread1_Sheet1.DataAutoSizeColumns = false;
            //for (int j = 0; j < this.neuSpread1_Sheet1.Rows.Count; j++)
            //{
            //    this.neuSpread1_Sheet1.Columns[j].Width = this.neuSpread1_Sheet1.Columns[j].GetPreferredWidth();
            //}

            //if(dsFeeStat.Tables[0].Rows.Count<=0)
            //{
            //     return -1;
            //}
            //SetReportColumnHeader();
            //SetReportData(dsResult.Tables[0], 0);

           

            //this.AddSumRow();

            return 1;
        }


        /// <summary>
        /// 显示数据
        /// </summary>
        /// <param name="dsResult"></param>
        public void ShowData(DataSet dsResult)
        {
            if (this.ReportType == "1")
            {
                DataTable dt = dsResult.Tables[0];

                this.neuSpread1_Sheet1.Rows.Count = 0;
                int index = this.neuSpread1_Sheet1.Rows.Count;

                foreach (DataRow dr in dt.Rows)
                {
                    this.neuSpread1_Sheet1.Rows.Add(index, 1);

                    FarPoint.Win.Spread.CellType.NumberCellType cellType = new FarPoint.Win.Spread.CellType.NumberCellType();

                    FarPoint.Win.Spread.CellType.TextCellType txtTppe = new FarPoint.Win.Spread.CellType.TextCellType();
                    this.neuSpread1_Sheet1.Cells[index, 0].CellType = txtTppe;
                    this.neuSpread1_Sheet1.Cells[index, 0].Text = dr[0].ToString().PadLeft(6, '0');
                    this.neuSpread1_Sheet1.Cells[index, 1].Text = dr[1].ToString();

                    this.neuSpread1_Sheet1.Cells[index, 2].CellType = cellType;
                    this.neuSpread1_Sheet1.Cells[index, 2].Text = dr[2].ToString();

                    this.neuSpread1_Sheet1.Cells[index, 3].CellType = cellType;
                    this.neuSpread1_Sheet1.Cells[index, 3].Text = dr[3].ToString();

                    this.neuSpread1_Sheet1.Cells[index, 4].CellType = cellType;
                    this.neuSpread1_Sheet1.Cells[index, 4].Text = dr[4].ToString();

                    this.neuSpread1_Sheet1.Cells[index, 5].CellType = cellType;
                    this.neuSpread1_Sheet1.Cells[index, 5].Text = dr[5].ToString();

                    this.neuSpread1_Sheet1.Cells[index, 6].CellType = cellType;
                    this.neuSpread1_Sheet1.Cells[index, 6].Text = dr[6].ToString();

                    this.neuSpread1_Sheet1.Cells[index, 7].CellType = cellType;
                    this.neuSpread1_Sheet1.Cells[index, 7].Text = dr[7].ToString();

                    this.neuSpread1_Sheet1.Cells[index, 8].CellType = cellType;
                    this.neuSpread1_Sheet1.Cells[index, 8].Text = dr[8].ToString();

                    this.neuSpread1_Sheet1.Cells[index, 9].CellType = cellType;
                    this.neuSpread1_Sheet1.Cells[index, 9].Text = dr[9].ToString();

                    this.neuSpread1_Sheet1.Cells[index, 10].CellType = cellType;
                    this.neuSpread1_Sheet1.Cells[index, 10].Text = dr[10].ToString();

                    this.neuSpread1_Sheet1.Cells[index, 11].CellType = cellType;
                    this.neuSpread1_Sheet1.Cells[index, 11].Text = dr[11].ToString();

                    this.neuSpread1_Sheet1.Cells[index, 12].CellType = cellType;
                    this.neuSpread1_Sheet1.Cells[index, 12].Text = dr[12].ToString();

                    this.neuSpread1_Sheet1.Cells[index, 13].CellType = cellType;
                    this.neuSpread1_Sheet1.Cells[index, 13].Text = dr[13].ToString();

                    this.neuSpread1_Sheet1.Cells[index, 14].CellType = cellType;
                    this.neuSpread1_Sheet1.Cells[index, 14].Text = dr[14].ToString();

                    index++;

                }
            }
            else if (this.ReportType == "2")
            {
                DataTable dt = dsResult.Tables[0];

                this.neuSpread1_Sheet1.Rows.Count = 0;
                int index = this.neuSpread1_Sheet1.Rows.Count;

                foreach (DataRow dr in dt.Rows)
                {
                    this.neuSpread1_Sheet1.Rows.Add(index, 1);

                    FarPoint.Win.Spread.CellType.NumberCellType cellType = new FarPoint.Win.Spread.CellType.NumberCellType();

                    FarPoint.Win.Spread.CellType.TextCellType txtTppe = new FarPoint.Win.Spread.CellType.TextCellType();

                    this.neuSpread1_Sheet1.Cells[index, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    this.neuSpread1_Sheet1.Cells[index, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                    this.neuSpread1_Sheet1.Cells[index, 0].Locked = true;
                    this.neuSpread1_Sheet1.Cells[index, 1].Locked = true;
                    this.neuSpread1_Sheet1.Cells[index, 2].Locked = true;
                    this.neuSpread1_Sheet1.Cells[index, 3].Locked = true;
                    this.neuSpread1_Sheet1.Cells[index, 4].Locked = true;
                    this.neuSpread1_Sheet1.Cells[index, 5].Locked = true;
                    this.neuSpread1_Sheet1.Cells[index, 6].Locked = true;
                    this.neuSpread1_Sheet1.Cells[index, 7].Locked = true;
                    this.neuSpread1_Sheet1.Cells[index, 8].Locked = true;

                    this.neuSpread1_Sheet1.Cells[index, 0].CellType = txtTppe;
                    this.neuSpread1_Sheet1.Cells[index, 0].Text = dr[0].ToString();

                    this.neuSpread1_Sheet1.Cells[index, 1].CellType = cellType;
                    this.neuSpread1_Sheet1.Cells[index, 1].Text = dr[1].ToString();

                    this.neuSpread1_Sheet1.Cells[index, 2].CellType = cellType;
                    this.neuSpread1_Sheet1.Cells[index, 2].Text = dr[2].ToString();

                    this.neuSpread1_Sheet1.Cells[index, 3].CellType = cellType;
                    this.neuSpread1_Sheet1.Cells[index, 3].Text = dr[3].ToString();

                    this.neuSpread1_Sheet1.Cells[index, 4].CellType = cellType;
                    this.neuSpread1_Sheet1.Cells[index, 4].Text = dr[4].ToString();

                    this.neuSpread1_Sheet1.Cells[index, 5].CellType = cellType;
                    this.neuSpread1_Sheet1.Cells[index, 5].Text = dr[5].ToString();

                    this.neuSpread1_Sheet1.Cells[index, 6].CellType = cellType;
                    this.neuSpread1_Sheet1.Cells[index, 6].Text = dr[6].ToString();

                    this.neuSpread1_Sheet1.Cells[index, 7].CellType = cellType;
                    this.neuSpread1_Sheet1.Cells[index, 7].Text = dr[7].ToString();

                    this.neuSpread1_Sheet1.Cells[index, 8].CellType = cellType;
                    this.neuSpread1_Sheet1.Cells[index, 8].Text = dr[8].ToString();

                    index++;
                }
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
                    continue;
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
            // 科室
            ArrayList arlDept = this.managerIntegrate.GetDeptmentAllValid();
            ArrayList arlDeptList = new ArrayList();
            switch (strPriv)
            {
                case "0":
                    FS.HISFC.Models.Base.Department dept = new FS.HISFC.Models.Base.Department();
                    dept.ID = "ALL";
                    dept.Name = "全部";
                    arlDeptList.Add(dept);

                    arlDeptList.AddRange(arlDept);
                    break;

                case "1":
                case "2":
                    foreach (FS.HISFC.Models.Base.Department dept1 in arlDept)
                    {
                        if (dept1.ID == employee.Dept.ID)
                        {
                            arlDeptList.Add(dept1);
                        }
                    }

                    break;

                default:
                    break;
            }
            this.cmbDept.AddItems(arlDeptList);
            this.cmbDept.SelectedIndex = 0;

            // 人员
            ArrayList arlEmployee = this.managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.F);
            ArrayList arlEmployeeList = new ArrayList();

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
            //iniColumnDdata();
            SetReportColumnHeader();
            this.filePath = Application.StartupPath + @".\profile\GYZLQueryMZInvoiceInfoByStartEnd.xml";
            if (System.IO.File.Exists(filePath))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.neuSpread1_Sheet1, this.filePath);
            }
            else
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.filePath);
            }

            return;

            base.OnLoad(e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryReport();
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            if (MessageBox.Show("是否打印?", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.No)
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
            int toPage = (System.Int32)Math.Ceiling((double)this.neuSpread1_Sheet1.Rows.Count / lineCount);

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
            this.neuSpread1.Export();
            return base.Export(sender, neuObject);
        }


        #endregion



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


        private void SetReportColumnHeader()
        {
            //------------------head
            this.neuSpread1_Sheet1.ColumnCount = 4;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].Text = "发票起止号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].Column.Width = 200F;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].Text = "无效张数";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].Column.Width = 100F;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].Text = "有效张数";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].Column.Width = 100F;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 3].Text = "有效金额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 3].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 3].Column.Width = 100F;

            //---------------------head      

        }

        private void SetReportData(DataTable table, int autoColCount)
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
            this.neuSpread1_Sheet1.Rows.Count += 1;
            //-----------data start
            DataView dvData = table.DefaultView;
            //发票起止号
            neuSpread1_Sheet1.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            neuSpread1_Sheet1.Cells[0, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            if (string.IsNullOrEmpty(this.tbInvoiceStart.Text.Trim()) || string.IsNullOrEmpty(this.tbInvoiceEnd.Text.Trim()))
            {
                neuSpread1_Sheet1.Cells[0, 0].Text = (dvData.ToTable().Select("print_invoiceno = min(print_invoiceno)"))[0]["print_invoiceno"].ToString() + "--" +
                    (dvData.ToTable().Select("print_invoiceno = max(print_invoiceno)"))[0]["print_invoiceno"].ToString();
                //neuSpread1_Sheet1.Cells[0, 0].Text = "全部";

            }
            else
            {
                neuSpread1_Sheet1.Cells[0, 0].Text = this.tbInvoiceStart.Text.Trim().PadLeft(12, '0') + "--"
                    + this.tbInvoiceEnd.Text.Trim().PadLeft(12, '0');
            }

            //退费、作废票据号张数
            dvData.RowFilter = "cancel_flag in('0','2','3') and trans_type='2'";
            neuSpread1_Sheet1.Cells[0, 1].Text = dvData.Count.ToString();


            //有效票据号张数
            dvData.RowFilter = "cancel_flag = '1' and trans_type='1'";
            neuSpread1_Sheet1.Cells[0, 2].Text = dvData.Count.ToString();

            //有效票据合计金额
            neuSpread1_Sheet1.Cells[0, 3].Text = dvData.ToTable().Compute("sum(tot_cost)", "").ToString();
            neuSpread1_Sheet1.Cells[0, 3].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();


            //----------data end
            //行合计
            neuSpread1_Sheet1.Rows.Count += 1;
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].Text = "合计:";
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            FarPoint.Win.Spread.CellType.NumberCellType cellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            for (int i = 1; i < neuSpread1_Sheet1.ColumnCount; i++)
            {
                string strFormula = "sum(" + (char)(65 + i) + "1:"
                                           + (char)(65 + i) + (neuSpread1_Sheet1.RowCount - 1).ToString()
                                    + ")";
                neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, i].Formula = strFormula;
                if (i == neuSpread1_Sheet1.ColumnCount - 1)
                {
                    neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, i].CellType = cellType;
                }
                neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }
        }

        private void iniColumnDdata()
        {
            if (string.IsNullOrEmpty(this.StatClass))
            {
                MessageBox.Show("请设置统计大类代码!，例如门诊发票StatClass设置为MZ01");
                return;
            }
            dsFeeStat = feeMgr.GetFeeStatNameByReportCode(this.StatClass);

            if (dsFeeStat == null || dsFeeStat.Tables[0].Rows.Count <= 0)
            {
                MessageBox.Show("请检查统计大类代码是不是没有维护或者已经作废！");
                return;
            }

            this.SetReportColumnHeader();
        }


    }
}
