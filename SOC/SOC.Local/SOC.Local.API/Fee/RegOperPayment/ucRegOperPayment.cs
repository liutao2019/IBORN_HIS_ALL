using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.API.Fee.RegOperPayment
{
    public partial class ucRegOperPayment : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucRegOperPayment()
        {
            InitializeComponent();
        }

        #region 变量

        FS.HISFC.BizLogic.Fee.FeeReport feeMgr = new FS.HISFC.BizLogic.Fee.FeeReport();
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
        FS.SOC.Local.API.Common.Report reportMgr = new FS.SOC.Local.API.Common.Report();
        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
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

            if (this.reportMgr.GetRegOperPayment(DateTime.ParseExact(this.beginDate.Text, "yyyy-MM-dd HH:mm:ss", null), DateTime.ParseExact(this.endDate.Text, "yyyy-MM-dd HH:mm:ss", null), ref dsResult) == -1)
            {
                MessageBox.Show(this.reportMgr.Err);
                return -1;
            }
            if (dsResult.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("此时间段没有数据");
                return 0;
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
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 7;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "时间范围:" + this.beginDate.Value.ToShortDateString() + "至" + this.endDate.Value.ToShortDateString();
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Font = new Font("宋体", 12);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ForeColor = Color.Black;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 7;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }

        private void SetColumnHeader()
        {
            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.ColumnCount = 7;
            this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "挂号员";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = "现金";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = "支票";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = "商行POS";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = "中行POS机";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = "优惠";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].Text = "合计";
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

            this.filePath = Application.StartupPath + @".\profile\GYZLRegSummary.xml";
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
            print.PrintPage(10, 10, this.neuPrint);

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
            int startRow = this.neuSpread1_Sheet1.RowCount + 1;//数据开始行
            int endRow = 0;//数据结束行
            //-----------data start
            FarPoint.Win.Spread.CellType.NumberCellType type = new FarPoint.Win.Spread.CellType.NumberCellType();
            foreach (DataRow dr in table.Rows)
            {
                this.neuSpread1_Sheet1.RowCount++;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = dr[0].ToString();
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = dr[1].ToString();
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].CellType = type;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].CellType = type;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].CellType = type;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].CellType = type;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = dr[2].ToString();
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].CellType = type;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].Text = dr[3].ToString();
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].CellType = type;

            }

            endRow = this.neuSpread1_Sheet1.RowCount;
            //增加合计行
            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "合计";
            for(int i = 1; i < this.neuSpread1_Sheet1.ColumnCount; i++)
            {
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, i].CellType = type;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, i].Formula = this.GetSumString(i, startRow, endRow);
            }

            //-----------data end
            setPerfectSize();
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
            this.neuSpread1_Sheet1.Columns[0].Width = 80F;
            float preferredWidth = 0;
            for (int i = 1; i < this.neuSpread1_Sheet1.ColumnCount; i++)
            {
                preferredWidth = this.neuSpread1_Sheet1.Columns[i].GetPreferredWidth();
                this.neuSpread1_Sheet1.Columns[i].Width = preferredWidth > 80F ? preferredWidth : 80F;
            }
        }
    }
}
