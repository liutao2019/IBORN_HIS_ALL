using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;

namespace FS.SOC.Local.API.Fee.RegInvoice
{
    public partial class ucQueryInvoiceInfoByStartEnd : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucQueryInvoiceInfoByStartEnd()
        {
            InitializeComponent();
        }

        #region 变量

        FS.HISFC.BizLogic.Fee.FeeReport feeMgr = new FS.HISFC.BizLogic.Fee.FeeReport();
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
        FS.SOC.Local.API.Common.Report reportMgr = new FS.SOC.Local.API.Common.Report();
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

            DataSet dsResult = new DataSet();
            //门诊挂号票据
            if ("reg".Equals(this.reportType))
            {
                if (this.rdTime.Checked)
                {
                    if (this.reportMgr.GetRegInvoiceInfo(DateTime.ParseExact(this.beginDate.Text, "yyyy-MM-dd HH:mm:ss", null), DateTime.ParseExact(this.endDate.Text, "yyyy-MM-dd HH:mm:ss", null), ref dsResult) == -1)
                    {
                        MessageBox.Show(this.reportMgr.Err);
                        return -1;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(this.tbInvoiceStart.Text.Trim()) || string.IsNullOrEmpty(this.tbInvoiceEnd.Text.Trim()))
                    {
                        MessageBox.Show("请输入起止发票号");
                        return -1;
                    }
                    if (this.reportMgr.GetRegInvoiceInfo(this.tbInvoiceStart.Text, this.tbInvoiceEnd.Text, ref dsResult) == -1)
                    {
                        MessageBox.Show(this.reportMgr.Err);
                        return -1;
                    }
                }
            }
            //门诊收费票据
            else if ("fee".Equals(this.reportType))
            {
                if (this.rdTime.Checked)
                {
                    if (this.reportMgr.GetFeeInvoiceInfo(DateTime.ParseExact(this.beginDate.Text, "yyyy-MM-dd HH:mm:ss", null), DateTime.ParseExact(this.endDate.Text, "yyyy-MM-dd HH:mm:ss", null), ref dsResult) == -1)
                    {
                        MessageBox.Show(this.reportMgr.Err);
                        return -1;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(this.tbInvoiceStart.Text.Trim()) || string.IsNullOrEmpty(this.tbInvoiceEnd.Text.Trim()))
                    {
                        MessageBox.Show("请输入起止发票号");
                        return -1;
                    }
                    if (this.reportMgr.GetFeeInvoiceInfo(this.tbInvoiceStart.Text, this.tbInvoiceEnd.Text, ref dsResult) == -1)
                    {
                        MessageBox.Show(this.reportMgr.Err);
                        return -1;
                    }
                }
            }
            else
            {
                MessageBox.Show("请设置ReportType");
                return -1;
            }
            SetReportData(dsResult.Tables[0]);
            
            return 1;
        }

        /// <summary>
        /// 设置表头
        /// by lizy
        /// </summary>
        private void SetHeader()
        {
            this.neuSpread1_Sheet1.ColumnHeader.Visible = false;
            this.neuSpread1_Sheet1.RowCount = 0;

            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = this.titleName;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Font = new Font("宋体", 12);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ForeColor = Color.Black;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 4;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.neuSpread1_Sheet1.RowCount++;
            if (rdTime.Checked)
            {
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "统计时间:" + this.beginDate.Value.ToShortDateString() + " 00:00:00" + "至" + this.endDate.Value.ToShortDateString() + " 23:59:59";
            }
            else
            {
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "发票起止号:" + this.tbInvoiceStart.Text + " - " + this.tbInvoiceEnd.Text;
            }
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Font = new Font("宋体", 12);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ForeColor = Color.Black;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 4;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }

        private void SetFooter()
        {
            int curRow = this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Cells[curRow, 0].ColumnSpan = 4;
            this.neuSpread1_Sheet1.Cells[curRow, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            string strReg = this.conMgr.Operator.Name;
            this.neuSpread1_Sheet1.Cells[curRow, 0].Text = "    打印人：" + conMgr.Operator.Name + "    打印时间：" + DateTime.Now;
        }

        private void SetColumnHeader()
        {
            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.ColumnCount = 4;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "发票起止号";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = "无效张数";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = "有效张数";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = "有效金额";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }

        private void setPerfectSize()
        {
            this.neuSpread1_Sheet1.Columns[0].Width = 180F;
            float preferredWidth = 0;
            for (int i = 1; i < this.neuSpread1_Sheet1.ColumnCount; i++)
            {
                preferredWidth = this.neuSpread1_Sheet1.Columns[i].GetPreferredWidth();
                this.neuSpread1_Sheet1.Columns[i].Width = preferredWidth > 100F ? preferredWidth : 100F;
            }
        }

        #endregion

        #region 事件

        protected override void OnLoad(EventArgs e)
        {
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ColumnHeader.Visible = false;

            if (this.DesignMode)
            {
                return;
            }

            DateTime now = feeMgr.GetDateTimeFromSysDateTime();
            this.beginDate.Value = now;
            this.endDate.Value = now;

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
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.neuSpread1_Sheet1.RowCount = 0;
            this.SetHeader();
            this.SetColumnHeader();
            this.QueryReport();
            this.SetFooter();
            this.setPerfectSize();
            return 0;
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
                ps = new FS.HISFC.Models.Base.PageSize();
            }

            print.SetPageSize(ps);
            print.PrintPage(0, 0, this.neuPrint);

            return 0;
        }

        public override int Export(object sender, object neuObject)
        {
            this.neuSpread1.Export();
            return 0;
        }


        #endregion

        private void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.filePath);
        }

        private void SetReportData(DataTable table)
        {
            if (table.Rows.Count == 0)
            {
                return;
            }
            //-----------data start

            FarPoint.Win.Spread.CellType.NumberCellType type = new FarPoint.Win.Spread.CellType.NumberCellType();

            string startNo;
            string endNo;
            int invalidCount = 0;   //无效张数
            int validCount = 0;     //有效张数
            decimal totFee = 0;     //有效金额
            DataRow dr = null;      //行记录

            //发票起止号
            if (this.rdTime.Checked)
            {
                //根据时间查询，存在多号段
                startNo = table.Rows[0][0].ToString();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    dr = table.Rows[i];
                    if ("1".Equals(dr[1].ToString()))
                    {
                        //有效
                        validCount++;
                    }
                    else if ("0".Equals(dr[1].ToString()) || "2".Equals(dr[1].ToString()) || "3".Equals(dr[1].ToString()))
                    {
                        //无效
                        invalidCount++;
                    }
                    totFee += decimal.Parse(dr[2].ToString());

                    if ((i + 1 < table.Rows.Count) && (this.isContiune(table.Rows[i], table.Rows[i + 1])))
                    {
                        //发票号连续，继续统计
                        continue;
                    }
                    else
                    {
                        //发票号不连续，记录
                        this.neuSpread1_Sheet1.RowCount++;
                        endNo = dr[0].ToString();
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = startNo + "-" + endNo;
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = invalidCount.ToString();
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = validCount.ToString();
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = totFee.ToString("F2");
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].CellType = type;
                        //统计清零
                        invalidCount = 0;
                        validCount = 0;
                        totFee = 0;
                        //重新获取开始发票号
                        if (i + 1 < table.Rows.Count)
                        {
                            startNo = table.Rows[i + 1][0].ToString();
                        }
                    }
                }
            }
            else
            {
                //根据发票起止号查询，单个号段
                startNo = table.Rows[0][0].ToString();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    dr = table.Rows[i];
                    if ("1".Equals(dr[1].ToString()))
                    {
                        //有效
                        validCount++;
                    }
                    else if ("0".Equals(dr[1].ToString()) || "2".Equals(dr[1].ToString()) || "3".Equals(dr[1].ToString()))
                    {
                        //无效
                        invalidCount++;
                    }
                    totFee += decimal.Parse(dr[2].ToString());
                }
                endNo = table.Rows[table.Rows.Count - 1][0].ToString();

                this.neuSpread1_Sheet1.RowCount++;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = startNo + "-" + endNo;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = invalidCount.ToString();
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = validCount.ToString();
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = totFee.ToString("F2");
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].CellType = type;
            }
            this.lockTable();
        }

        private void lockTable()
        {
            int rowCount = this.neuSpread1_Sheet1.RowCount;
            for (int i = 0; i < rowCount; i++)
            {
                this.neuSpread1_Sheet1.Rows[i].Locked = true;
            }
        }

        private bool isContiune(DataRow first, DataRow second)
        {
            if (long.Parse(getNumber(first[0].ToString())) + 1 == long.Parse(getNumber(second[0].ToString())))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string getNumber(string text)
        {
            string temp = Regex.Replace(text, @"^0*", "");
            temp = Regex.Replace(temp, "[a-zA-z]*", "");
            return temp;
        }
    }
}
