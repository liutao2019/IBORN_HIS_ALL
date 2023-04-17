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

namespace FS.SOC.Local.API.Fee.RegOperPaymentDetail
{
    public partial class ucRegOperPaymentDetail : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucRegOperPaymentDetail()
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

        [Category("报表设置"), Description("是否显示挂号员")]
        public bool IsOperVisible
        {
            get { return this.cmbRegOper.Visible; }
            set
            {
                this.cmbRegOper.Visible = value;
                this.lbOper.Visible = value;
            }
        }

        private string filePath = string.Empty;

        #endregion

        #region  方法

        public int QueryReport()
        {

            DataSet dsResult = new DataSet();

            string strOperID = this.conMgr.Operator.ID;

            if (this.cmbRegOper.Visible == true)
            {
                strOperID = this.cmbRegOper.SelectedItem.ID;
            }

            if (this.reportMgr.GetRegOperPaymentDetail(strOperID, DateTime.ParseExact(this.beginDate.Text, "yyyy-MM-dd HH:mm:ss", null), DateTime.ParseExact(this.endDate.Text, "yyyy-MM-dd HH:mm:ss", null), ref dsResult) == -1)
            {
                MessageBox.Show(this.reportMgr.Err);
                return -1;
            }
            if (dsResult.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("此时间段没有数据");
                return 0;
            }
            this.SetReportData(dsResult.Tables[0]);
            this.QueryInvalid();
            this.SetSecondColumnHeader();
            this.QueryDeptReport();
            this.SetFooter();
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
            this.neuSpread1_Sheet1.ColumnCount = 10;
            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = this.titleName;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Font = new Font("宋体", 12);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ForeColor = Color.Black;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 10;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.neuSpread1_Sheet1.RowCount++;
            string strReg = this.conMgr.Operator.Name;
            if (this.cmbRegOper.Visible == true)
            {
                strReg = this.cmbRegOper.SelectedItem.Name;
            }
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "统计时间:" + this.beginDate.Value.ToShortDateString() + " 00:00:00" + "至" + this.endDate.Value.ToShortDateString() + " 23:59:59" + "\t挂号员:" + strReg;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Font = new Font("宋体", 12);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ForeColor = Color.Black;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 10;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }

        private void SetFooter()
        {
            int curRow = this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Cells[curRow, 0].ColumnSpan = 10;
            this.neuSpread1_Sheet1.Cells[curRow, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            string strReg = this.conMgr.Operator.Name;
            this.neuSpread1_Sheet1.Cells[curRow, 0].Text = "    打印人：" + conMgr.Operator.Name + "    打印时间：" + DateTime.Now;
        }

        //设置第一个列头
        private void SetFirstColumnHeader()
        {
            this.neuSpread1_Sheet1.RowCount++;
            int curRow = this.neuSpread1_Sheet1.RowCount - 1;
            this.neuSpread1_Sheet1.Rows[curRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Rows[curRow].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[curRow, 0].Text = "发票起止";
            this.neuSpread1_Sheet1.Cells[curRow, 1].Text = "发票张数";
            this.neuSpread1_Sheet1.Cells[curRow, 2].Text = "挂号费";
            this.neuSpread1_Sheet1.Cells[curRow, 3].Text = "诊金";
            this.neuSpread1_Sheet1.Cells[curRow, 4].Text = "门诊病历费";
            this.neuSpread1_Sheet1.Cells[curRow, 5].Text = "金额合计";
            this.neuSpread1_Sheet1.Cells[curRow, 6].Text = "现金";
            this.neuSpread1_Sheet1.Cells[curRow, 7].Text = "商行POS机";
            this.neuSpread1_Sheet1.Cells[curRow, 8].Text = "中行POS机";
            this.neuSpread1_Sheet1.Cells[curRow, 9].Text = "优惠";
        }

        //设置第二个列头
        private void SetSecondColumnHeader()
        {
            this.neuSpread1_Sheet1.RowCount++;
            int curRow = this.neuSpread1_Sheet1.RowCount - 1;
            this.neuSpread1_Sheet1.Rows[curRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Rows[curRow].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[curRow, 0].Text = "科室";
            this.neuSpread1_Sheet1.Cells[curRow, 1].Text = "发票张数";
            this.neuSpread1_Sheet1.Cells[curRow, 2].Text = "挂号费";
            this.neuSpread1_Sheet1.Cells[curRow, 3].Text = "诊金";
            this.neuSpread1_Sheet1.Cells[curRow, 4].Text = "门诊病历费";
            this.neuSpread1_Sheet1.Cells[curRow, 5].Text = "金额合计";
            this.neuSpread1_Sheet1.Cells[curRow, 6].Text = "现金";
            this.neuSpread1_Sheet1.Cells[curRow, 7].Text = "商行POS机";
            this.neuSpread1_Sheet1.Cells[curRow, 8].Text = "中行POS机";
            this.neuSpread1_Sheet1.Cells[curRow, 9].Text = "优惠";
        }

        private void SetReportData(DataTable table)
        {
            int startRow = this.neuSpread1_Sheet1.RowCount + 1;//数据开始行
            int endRow = 0;//数据结束行
            //统计
            int count = 0;
            decimal regFeeSum = 0;
            decimal diagFeeSum = 0;
            decimal othFeeSum = 0;
            decimal totFeeSum = 0;
            decimal cashFeeSum = 0;
            decimal discountFeeSum = 0;
            //汇总
            string startNo = "";
            string endNo = "";
            //-----------data start
            FarPoint.Win.Spread.CellType.NumberCellType type = new FarPoint.Win.Spread.CellType.NumberCellType();

            if (table.Rows.Count == 0)
            {
                return;
            }
            startNo = table.Rows[0][0].ToString();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow dr = table.Rows[i];
                //统计
                count++;
                regFeeSum += decimal.Parse(dr[1].ToString());
                diagFeeSum += decimal.Parse(dr[2].ToString());
                othFeeSum += decimal.Parse(dr[3].ToString());
                totFeeSum += decimal.Parse(dr[4].ToString());
                cashFeeSum += decimal.Parse(dr[5].ToString());
                discountFeeSum += decimal.Parse(dr[6].ToString());

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
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = count.ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = regFeeSum.ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].CellType = type;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = diagFeeSum.ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].CellType = type;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = othFeeSum.ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].CellType = type;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = totFeeSum.ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].CellType = type;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].Text = cashFeeSum.ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].CellType = type;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 7].Text = "0.00";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 7].CellType = type;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 8].Text = "0.00";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 8].CellType = type;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 9].Text = discountFeeSum.ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 9].CellType = type;
                    //统计清零
                    count = 0;
                    regFeeSum = 0;
                    diagFeeSum = 0;
                    othFeeSum = 0;
                    totFeeSum = 0;
                    cashFeeSum = 0;
                    discountFeeSum = 0;
                    //重新获取开始发票号
                    if (i + 1 < table.Rows.Count)
                    {
                        startNo = table.Rows[i + 1][0].ToString();
                    }
                }
            }
            endRow = this.neuSpread1_Sheet1.RowCount;
            //增加合计行
            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "合计";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Formula = this.GetSumString(1, startRow, endRow);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Tag = "total";
            for (int i = 2; i < this.neuSpread1_Sheet1.ColumnCount; i++)
            {
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, i].CellType = type;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, i].Formula = this.GetSumString(i, startRow, endRow);
            }
            //-----------data end
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

        private string GetSumString(int column, int startRow, int endRow)
        {
            string result = "SUM({0}{1}:{0}{2})";
            //计算列号
            char index = 'A';
            result = string.Format(result, (char)(index + column), startRow, endRow);
            return result;
        }

        private void setPerfectSize()
        {
            this.neuSpread1_Sheet1.Columns[0].Width = 200F;
            float preferredWidth = 0;
            for (int i = 1; i < this.neuSpread1_Sheet1.ColumnCount; i++)
            {
                preferredWidth = this.neuSpread1_Sheet1.Columns[i].GetPreferredWidth();
                this.neuSpread1_Sheet1.Columns[i].Width = preferredWidth > 80F ? preferredWidth : 80F;
            }
        }

        private void QueryInvalid()
        {
            this.neuSpread1_Sheet1.RowCount++;
            string strRegID = this.conMgr.Operator.ID;
            if (this.cmbRegOper.Visible == true)
            {
                strRegID = this.cmbRegOper.SelectedItem.ID;
            }
            ArrayList invalidList = reportMgr.GetRegOperInvalidReg(strRegID, DateTime.ParseExact(this.beginDate.Text, "yyyy-MM-dd HH:mm:ss", null), DateTime.ParseExact(this.endDate.Text, "yyyy-MM-dd HH:mm:ss", null));

            string result = "有效发票共{0}张，作废发票共{1}张{2}{3}";
            string strInvalid = string.Empty;//{3}无效发票号
            string strSymbol = "。";//{2}符号
            foreach (FS.FrameWork.Models.NeuObject obj in invalidList)
            {
                strInvalid = strInvalid + obj.Name + ",";
            }
            if (invalidList.Count > 0)
            {
                strInvalid = strInvalid.Substring(0, strInvalid.Length - 1);//去掉最后的","
                strSymbol = ":";
            }
            result = string.Format(result, int.Parse(this.neuSpread1_Sheet1.Cells["total"].Text) - invalidList.Count, invalidList.Count, strSymbol, strInvalid);

            int curRow = this.neuSpread1_Sheet1.RowCount - 1;
            this.neuSpread1_Sheet1.Cells[curRow, 0].ColumnSpan = 10;
            this.neuSpread1_Sheet1.Cells[curRow, 0].Text = result;
            this.neuSpread1_Sheet1.Cells[curRow, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            FarPoint.Win.Spread.CellType.TextCellType type = new FarPoint.Win.Spread.CellType.TextCellType();
            type.WordWrap = true;
            this.neuSpread1_Sheet1.Cells[curRow, 0].CellType = type;
            this.neuSpread1_Sheet1.Rows[curRow].Height = this.neuSpread1_Sheet1.Rows[curRow].GetPreferredHeight();
        }

        private int QueryDeptReport()
        {
            DataSet dsResult = new DataSet();

            string strRegID = this.conMgr.Operator.ID;
            if (this.cmbRegOper.Visible == true)
            {
                strRegID = this.cmbRegOper.SelectedItem.ID;
            }
            if (this.reportMgr.GetRegOperPaymentDeptDetail(strRegID, DateTime.ParseExact(this.beginDate.Text, "yyyy-MM-dd HH:mm:ss", null), DateTime.ParseExact(this.endDate.Text, "yyyy-MM-dd HH:mm:ss", null), ref dsResult) == -1)
            {
                MessageBox.Show(this.reportMgr.Err);
                return -1;
            }
            if (dsResult.Tables[0].Rows.Count == 0)
            {
                return 0;
            }
            SetDeptReportData(dsResult.Tables[0]);
            return 1;
        }

        private void SetDeptReportData(DataTable table)
        {
            int startRow = this.neuSpread1_Sheet1.RowCount + 1;//数据开始行
            int endRow = 0;//数据结束行
            //-----------data start
            FarPoint.Win.Spread.CellType.NumberCellType type = new FarPoint.Win.Spread.CellType.NumberCellType();
            foreach (DataRow dr in table.Rows)
            {
                this.neuSpread1_Sheet1.RowCount++;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = dr[0].ToString();
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = dr[1].ToString();
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = dr[2].ToString();
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].CellType = type;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = dr[3].ToString();
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].CellType = type;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = dr[4].ToString();
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].CellType = type;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = dr[5].ToString();
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].CellType = type;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].Text = dr[6].ToString();
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].CellType = type;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 7].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 7].CellType = type;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 8].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 8].CellType = type;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 9].Text = dr[7].ToString();
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 9].CellType = type;
            }
            endRow = this.neuSpread1_Sheet1.RowCount;
            //增加合计行
            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "合计";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Formula = this.GetSumString(1, startRow, endRow);
            for (int i = 2; i < this.neuSpread1_Sheet1.ColumnCount; i++)
            {
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, i].CellType = type;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, i].Formula = this.GetSumString(i, startRow, endRow);
            }
            //-----------data end
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

            //获取时间
            DateTime now = feeMgr.GetDateTimeFromSysDateTime();
            this.beginDate.Value = now;
            this.endDate.Value = now;
            //获取挂号员
            ArrayList OperList = reportMgr.GetRegOperList();
            this.cmbRegOper.AddItems(OperList);
            if (OperList != null && OperList.Count != 0)
            {
                this.cmbRegOper.SelectedIndex = 0;
            }
            this.filePath = Application.StartupPath + @".\profile\GYZLRegOperPaymentDetail.xml";
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
            this.SetFirstColumnHeader();
            this.QueryReport();

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
            ps = new FS.HISFC.Models.Base.PageSize("A4", this.Width, this.Height); ;

            if (ps == null)
            {
                //默认为A4纸张
                ps = new FS.HISFC.Models.Base.PageSize();
            }

            print.SetPageSize(ps);
            print.IsLandScape = true;
            print.PrintPage(120, 10, this.neuPrint);

            return 0;
        }

        public override int Export(object sender, object neuObject)
        {
            this.neuSpread1.Export();
            return 0;
        }

        private void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.filePath);
        }
        #endregion


    }
}
