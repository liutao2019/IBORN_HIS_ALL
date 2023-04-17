using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Function;

namespace FS.SOC.Local.OutpatientFee.ZDWY
{
    /// <summary>
    /// 门诊收入发票分类汇总日报表
    /// </summary>
    public partial class ucOutPatientInvoiceIncome : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ucOutPatientInvoiceIncome()
        {
            InitializeComponent();
        }

        #region 变量和属性

        /// <summary>
        /// 门诊发票明细汇总
        /// </summary>
        private string strSqlInvoiceDetail = "FS.HIS.OutPatient.Income.InvoiceDetail.CostSum";

        /// <summary>
        /// 门诊支付方式明细
        /// </summary>
        private string strSqlInvoicePayModeDetail = "FS.HIS.OutPatient.Income.InvoicePayModeDetail.CostSum";

        /// <summary>
        /// 门诊支付方式汇总
        /// </summary>
        private string strSqlInvoicePayModeTot = "FS.HIS.OutPatient.Income.InvoicePayModeTot.CostSum";

        /// <summary>
        /// 门诊结算明细
        /// </summary>
        private string strSqlInvoicePactDetail = "FS.HIS.OutPatient.Income.InvoicePactDetail.CostSum";

        /// <summary>
        /// 门诊结算汇总
        /// </summary>
        private string strSqlInvoicePactTot = "FS.HIS.OutPatient.Income.InvoicePactTot.CostSum";

        /// <summary>
        /// 门诊记账明细
        /// </summary>
        private string strSqlInvoicePBDetail = "FS.HIS.OutPatient.Income.InvoicePBDetail.CostSum";

        /// <summary>
        /// 门诊记账汇总
        /// </summary>
        private string strSqlInvoicePBTot = "FS.HIS.OutPatient.Income.InvoicePBTot.CostSum";

        /// <summary>
        /// 门诊费用业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Outpatient outFee = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// 默认数据源
        /// </summary>
        object defaultDataSource = null;

        #endregion


        #region 方法和事件

        /// <summary>
        /// 初始化加载事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            //设置日期
            DateTime dtNow = this.outFee.GetDateTimeFromSysDateTime();
            this.dtBeginDate.Value = dtNow.Date;
            this.dtEndDate.Value = dtNow.Date;
            this.defaultDataSource = this.neuSpread1_Sheet1.DataSource;

            base.OnLoad(e);
        }

        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear()
        {
            DateTime dtNow = this.outFee.GetDateTimeFromSysDateTime();
            this.neuSpread1_Sheet1.DataSource = this.defaultDataSource;
            this.neuSpread1_Sheet1.Rows.Count = 6;
            this.neuSpread1_Sheet1.Cells[5, 1].Text = "";
            this.neuSpread1_Sheet1.Cells[5, 5].Text = "";
            this.neuSpread1_Sheet1.Cells[5, 11].Text = "";

            //设置页脚
            this.neuSpread1_Sheet1.Rows.Count = 7;
            this.neuSpread1_Sheet1.Cells[6, 0].ColumnSpan = 4;
            this.neuSpread1_Sheet1.Cells[6, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Cells[6, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[6, 0].Text = "复核：";
            this.neuSpread1_Sheet1.Cells[6, 4].ColumnSpan = 6;
            this.neuSpread1_Sheet1.Cells[6, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Cells[6, 4].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[6, 4].Text = "打印日期：" + dtNow.ToString();
            this.neuSpread1_Sheet1.Cells[6, 10].ColumnSpan = 5;
            this.neuSpread1_Sheet1.Cells[6, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.neuSpread1_Sheet1.Cells[6, 10].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[6, 10].Text = "收费窗口组长：" + FS.FrameWork.Management.Connection.Operator.Name;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            //清屏
            this.Clear();
            DateTime dtNow = this.outFee.GetDateTimeFromSysDateTime();

            DateTime dateBegin = this.dtBeginDate.Value.Date;
            DateTime dateEnd = this.dtEndDate.Value.Date.AddDays(1).AddSeconds(-1); //当天的23:59:59

            //字体
            System.Drawing.Font tFB = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            

            //设置查询日期
            this.neuSpread1_Sheet1.Cells[1, 10].Text = "报表日期：" + dateBegin.ToShortDateString() + " 至 " + dateEnd.ToShortDateString();
            
            //门诊发票明细
            DataSet dsInvoiceDetail = new DataSet();
            if (this.outFee.ExecQuery(this.strSqlInvoiceDetail, ref dsInvoiceDetail, dateBegin.ToString(), dateEnd.ToString()) == -1)
            {
                MessageBox.Show("执行查询门诊发票明细错误" + this.outFee.Err);
                return -1;
            }
            if (dsInvoiceDetail == null || dsInvoiceDetail.Tables.Count <= 0 || dsInvoiceDetail.Tables[0].Rows.Count <= 0)
            {
                MessageBox.Show("该时间内没有门诊发票汇总报表!" + this.outFee.Err);
                return -1;
            }
            DataTable dtInvoiceDetail = dsInvoiceDetail.Tables[0];
            foreach (DataRow dr in dtInvoiceDetail.Rows)
            {
                this.neuSpread1_Sheet1.Cells[4, 0].Text = dr[0].ToString();
                this.neuSpread1_Sheet1.Cells[4, 1].Text = dr[1].ToString();
                this.neuSpread1_Sheet1.Cells[4, 2].Text = dr[2].ToString();
                this.neuSpread1_Sheet1.Cells[4, 3].Text = dr[3].ToString();
                this.neuSpread1_Sheet1.Cells[4, 4].Text = dr[4].ToString();
                this.neuSpread1_Sheet1.Cells[4, 5].Text = dr[5].ToString();
                this.neuSpread1_Sheet1.Cells[4, 6].Text = dr[6].ToString();
                this.neuSpread1_Sheet1.Cells[4, 7].Text = dr[7].ToString();
                this.neuSpread1_Sheet1.Cells[4, 8].Text = dr[8].ToString();
                this.neuSpread1_Sheet1.Cells[4, 9].Text = dr[9].ToString();
                this.neuSpread1_Sheet1.Cells[4, 10].Text = dr[10].ToString();
                this.neuSpread1_Sheet1.Cells[4, 11].Text = dr[11].ToString();
                this.neuSpread1_Sheet1.Cells[4, 12].Text = dr[12].ToString();
                this.neuSpread1_Sheet1.Cells[4, 13].Text = dr[13].ToString();

                this.neuSpread1_Sheet1.Cells[3, 14].Font = tFB;
                this.neuSpread1_Sheet1.Cells[3, 14].Text = dr[14].ToString();  //合计
            }

            //门诊发票支付方式明细
            DataSet dsInvoicePayModeDetail = new DataSet();
            if (this.outFee.ExecQuery(this.strSqlInvoicePayModeDetail, ref dsInvoicePayModeDetail, dateBegin.ToString(), dateEnd.ToString()) == -1)
            {
                MessageBox.Show("执行查询门诊支付方式明细错误" + this.outFee.Err);
                return -1;
            }
            if (dsInvoicePayModeDetail == null || dsInvoicePayModeDetail.Tables.Count <= 0)
            {
                MessageBox.Show("该时间内没有门诊发票支付方式明细" + this.outFee.Err);
                return -1;
            }
            DataTable dtInvoicePayModeDetail = dsInvoicePayModeDetail.Tables[0];

            //门诊发票支付方式汇总
            DataSet dsInvoicePayModeTot = new DataSet();
            if (this.outFee.ExecQuery(this.strSqlInvoicePayModeTot, ref dsInvoicePayModeTot, dateBegin.ToString(), dateEnd.ToString()) == -1)
            {
                MessageBox.Show("执行查询门诊发票支付方式汇总错误" + this.outFee.Err);
                return -1;
            }
            if (dsInvoicePayModeTot == null || dsInvoicePayModeTot.Tables.Count <= 0)
            {
                MessageBox.Show("该时间内没有门诊发票支付方式汇总!" + this.outFee.Err);
                return -1;
            }
            DataTable dtInvoicePayModeTot = dsInvoicePayModeTot.Tables[0];

            //门诊结算明细
            DataSet dsInvoicePactDetail = new DataSet();
            if (this.outFee.ExecQuery(this.strSqlInvoicePactDetail, ref dsInvoicePactDetail, dateBegin.ToString(), dateEnd.ToString()) == -1)
            {
                MessageBox.Show("执行查询门诊结算明细错误" + this.outFee.Err);
                return -1;
            }
            if (dsInvoicePactDetail == null || dsInvoicePactDetail.Tables.Count <= 0)
            {
                MessageBox.Show("该时间内没有门诊结算明细!" + this.outFee.Err);
                return -1;
            }
            DataTable dtInvoicePactDetail = dsInvoicePactDetail.Tables[0];

            //门诊结算汇总
            DataSet dsInvoicePactTot = new DataSet();
            if (this.outFee.ExecQuery(this.strSqlInvoicePactTot, ref dsInvoicePactTot, dateBegin.ToString(), dateEnd.ToString()) == -1)
            {
                MessageBox.Show("执行查询门诊结算汇总错误" + this.outFee.Err);
                return -1;
            }
            if (dsInvoicePactTot == null || dsInvoicePactTot.Tables.Count <= 0)
            {
                MessageBox.Show("该时间内没有门诊结算汇总!" + this.outFee.Err);
                return -1;
            }
            DataTable dtInvoicePactTot = dsInvoicePactTot.Tables[0];

            //门诊记账明细
            DataSet dsInvoicePBDetail = new DataSet();
            if (this.outFee.ExecQuery(this.strSqlInvoicePBDetail, ref dsInvoicePBDetail, dateBegin.ToString(), dateEnd.ToString()) == -1)
            {
                MessageBox.Show("执行查询门诊记账明细错误" + this.outFee.Err);
                return -1;
            }
            if (dsInvoicePBDetail == null || dsInvoicePBDetail.Tables.Count <= 0)
            {
                MessageBox.Show("该时间内没有门诊记账明细!" + this.outFee.Err);
                return -1;
            }
            DataTable dtInvoicePBDetail = dsInvoicePBDetail.Tables[0];

            //门诊记账汇总
            DataSet dsInvoicePBTot = new DataSet();
            if (this.outFee.ExecQuery(this.strSqlInvoicePBTot, ref dsInvoicePBTot, dateBegin.ToString(), dateEnd.ToString()) == -1)
            {
                MessageBox.Show("执行查询门诊记账明细错误" + this.outFee.Err);
                return -1;
            }
            if (dsInvoicePBTot == null || dsInvoicePBTot.Tables.Count <= 0)
            {
                MessageBox.Show("该时间内没有门诊记账明细!" + this.outFee.Err);
                return -1;
            }
            DataTable dtInvoicePBTot = dsInvoicePBTot.Tables[0];

            //找出最大行数 - 门诊收费组长强烈要求去掉结算明细栏
            int rows1 = dtInvoicePayModeDetail.Rows.Count;
            //int rows2 = dtInvoicePactDetail.Rows.Count;
            int rows3 = NConvert.ToInt32(Math.Ceiling(NConvert.ToDecimal(dtInvoicePBDetail.Rows.Count) / 2));//记账明细一行两条记录
            int rows = Math.Max(rows1, rows3);  //Math.Max(Math.Max(rows1, rows2), rows3);
            this.neuSpread1_Sheet1.Rows.Add(6, rows);
            this.neuSpread1_Sheet1.Cells[5, 0].RowSpan = rows + 1;
            this.neuSpread1_Sheet1.Cells[5, 4].RowSpan = rows + 1;
            this.neuSpread1_Sheet1.Cells[5, 10].RowSpan = rows + 1;     //实际总行数=rows + 5(表头)

            
            //门诊支付方式
            //明细
            int index = 5;
            foreach (DataRow dr in dtInvoicePayModeDetail.Rows)
            {
                this.neuSpread1_Sheet1.Cells[index, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.neuSpread1_Sheet1.Cells[index, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[index, 1].ColumnSpan = 3;
                this.neuSpread1_Sheet1.Cells[index, 1].Text = dr[0].ToString();
                index++;
            }
            //合计
            foreach (DataRow dr in dtInvoicePayModeTot.Rows)
            {
                this.neuSpread1_Sheet1.Cells[index, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.neuSpread1_Sheet1.Cells[index, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
                this.neuSpread1_Sheet1.Cells[index, 1].ColumnSpan = 3;
                this.neuSpread1_Sheet1.Cells[index, 1].RowSpan = (rows + 1) - rows1;
                this.neuSpread1_Sheet1.Cells[index, 1].Text = dr[0].ToString();
                index++;
            }

            //门诊结算
            //index = 5;
            //foreach (DataRow dr in dtInvoicePactDetail.Rows)
            //{
            //    this.neuSpread1_Sheet1.Cells[index, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            //    this.neuSpread1_Sheet1.Cells[index, 5].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.Cells[index, 5].ColumnSpan = 5;
            //    this.neuSpread1_Sheet1.Cells[index, 5].Text = dr[0].ToString();
            //    index++;
            //}
            //foreach (DataRow dr in dtInvoicePactTot.Rows)
            //{
            //    this.neuSpread1_Sheet1.Cells[index, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            //    this.neuSpread1_Sheet1.Cells[index, 5].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
            //    this.neuSpread1_Sheet1.Cells[index, 5].ColumnSpan = 5;
            //    this.neuSpread1_Sheet1.Cells[index, 5].RowSpan = (rows + 1) - rows2;
            //    this.neuSpread1_Sheet1.Cells[index, 5].Text = dr[0].ToString();
            //    index++;
            //}

            //门诊记账
            //明细
            index = 5;
            int startIndex = 0;
            foreach (DataRow dr in dtInvoicePBDetail.Rows)
            {
                if ((startIndex % 2) == 0)
                {
                    //奇数列
                    this.neuSpread1_Sheet1.Cells[index, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                    this.neuSpread1_Sheet1.Cells[index, 5].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    this.neuSpread1_Sheet1.Cells[index, 5].ColumnSpan = 5;
                    this.neuSpread1_Sheet1.Cells[index, 5].Text = dr[0].ToString();

                    startIndex++;
                }
                else
                {
                    //偶数列
                    this.neuSpread1_Sheet1.Cells[index, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                    this.neuSpread1_Sheet1.Cells[index, 11].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    this.neuSpread1_Sheet1.Cells[index, 11].ColumnSpan = 4;
                    this.neuSpread1_Sheet1.Cells[index, 11].Text = dr[0].ToString();

                    startIndex++;
                    index++;
                }
            }
            //合计
            foreach (DataRow dr in dtInvoicePBTot.Rows)  //只有一行记录
            {
                this.neuSpread1_Sheet1.Cells[index, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.neuSpread1_Sheet1.Cells[index, 11].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
                this.neuSpread1_Sheet1.Cells[index, 11].ColumnSpan = 4;
                this.neuSpread1_Sheet1.Cells[index, 11].RowSpan = (rows + 6) - index;
                this.neuSpread1_Sheet1.Cells[index, 11].Text = dr[0].ToString();

                //奇数列需要合并
                if (!string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[index, 5].Text))
                {
                    //非空
                    this.neuSpread1_Sheet1.Cells[index + 1, 5].ColumnSpan = 5;
                    this.neuSpread1_Sheet1.Cells[index + 1, 5].RowSpan = (rows + 6) - (index + 1);

                }
                else
                {
                    //为空
                    this.neuSpread1_Sheet1.Cells[index, 5].ColumnSpan = 5;
                    this.neuSpread1_Sheet1.Cells[index, 5].RowSpan = (rows + 6) - index;
                }

                index++;
            }

            //页脚
            index = this.neuSpread1_Sheet1.Rows.Count - 1;
            this.neuSpread1_Sheet1.Cells[index, 0].Text = "复核：";
            this.neuSpread1_Sheet1.Cells[index, 4].Text = "打印日期：" + dtNow.ToString();
            this.neuSpread1_Sheet1.Cells[index, 10].Text = "收费窗口组长：" + FS.FrameWork.Management.Connection.Operator.Name;

            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize("", 1140, 830);
            p.IsLandScape = true;
            p.PrintPage(0, 0, this.gbMain);
            return base.OnPrint(sender, neuObject);
        }

        #endregion



    }
}
