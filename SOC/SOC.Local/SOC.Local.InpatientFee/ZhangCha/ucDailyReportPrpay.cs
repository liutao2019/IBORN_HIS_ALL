using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.InpatientFee.ZhangCha
{
    public partial class ucDailyReportPrpay : Base.ucDeptTimeBaseReport
    {
        public ucDailyReportPrpay()
        {
            InitializeComponent();
            //this.LeftAdditionTitle = "";
            this.OperationStartHandler += new DelegateOperationStart(ucDailyReportPrepay_DelegateOperateionStart);
            this.OperationEndHandler += new DelegateOperateionEnd(ucDailyReportPrepay_DelegateOperateionEnd);
            this.numericUpDown1.ValueChanged += new EventHandler(numericUpDown1_ValueChanged);
            this.ncmbTime.SelectedIndexChanged += new EventHandler(ncmbTime_SelectedIndexChanged);
            this.Load += new EventHandler(ucDailyReportPrepay_Load);
        }

        void ncmbTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strTemp = this.ncmbTime.Text;
            string[] strArr = strTemp.Split(new string[] { "到" }, StringSplitOptions.RemoveEmptyEntries);
            if (strArr == null || strArr.Length != 2)
            {
                return;
            }

            try
            {
                dtStart.Value = FS.FrameWork.Function.NConvert.ToDateTime(strArr[0]);
            }
            catch
            {
                dtStart.Value = DateTime.Now.AddYears(-10);
            }
            dtEnd.Value = FS.FrameWork.Function.NConvert.ToDateTime(strArr[1]);
        }

        private void SetTime()
        {
            DataSet ds = new DataSet();
            FS.FrameWork.Management.DataBaseManger dataBaseManger = new FS.FrameWork.Management.DataBaseManger();
            if (dataBaseManger.ExecQuery(string.Format(this.GetStaticTimeSQL, FS.FrameWork.Management.Connection.Operator.ID, this.numericUpDown1.Value.ToString()), ref ds) == -1)
            {
                this.ShowBalloonTip(10, "错误", "获取结算时间段发生错误！");
                this.ncmbTime.Visible = false;
            }
            if (ds != null && ds.Tables.Count > 0)
            {
                System.Collections.ArrayList al = new System.Collections.ArrayList();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    FS.FrameWork.Models.NeuObject o = new FS.FrameWork.Models.NeuObject();
                    o.ID = row[0].ToString();
                    o.Name = row[0].ToString();
                    o.Memo = row[0].ToString();
                    al.Add(o);
                }
                this.ncmbTime.AddItems(al);
                if (al.Count > 0)
                {
                    this.ncmbTime.SelectedIndex = 0;

                }
            }

        }

        /// <summary>
        /// 从第一个FarPoint中的值计算出报表数据，并用第二个FarPoint设置
        /// </summary>
        private void SetReportValue()
        {
            this.fpSpread1_Sheet2.DataSource = null;
            this.fpSpread1_Sheet2.ColumnCount = 6;
            this.fpSpread1_Sheet2.RowCount = 8;

            this.fpSpread1_Sheet2.Columns[0].Label = "预收收入";
            this.fpSpread1_Sheet2.Columns[1].Label = "现金";
            this.fpSpread1_Sheet2.Columns[2].Label = "支票";
            this.fpSpread1_Sheet2.Columns[3].Label = "信用卡";
            this.fpSpread1_Sheet2.Columns[4].Label = "其他";
            this.fpSpread1_Sheet2.Columns[5].Label = "外币转人民币";

            this.fpSpread1_Sheet2.ColumnHeader.Rows[0].Height = 36f;
            this.fpSpread1_Sheet2.ColumnHeader.Rows[0].Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Regular);

            //if (this.fpSpread1_Sheet1.RowCount == 0)
            //{
            //    return;
            //}


            DataTable dtData = null;
            if (this.DataView != null)
            {
                dtData = this.DataView.Table;
            }

            if (dtData != null)
            {
                // 预收收入计算
                decimal totPrepayCost = 0;
                totPrepayCost = FS.FrameWork.Function.NConvert.ToDecimal(dtData.Compute("Sum(prepay_cost)", ""));
                this.fpSpread1_Sheet2.Cells[0, 0].Value = totPrepayCost;
                this.fpSpread1_Sheet2.Cells[0, 0].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                this.fpSpread1_Sheet2.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                decimal decCA = 0;
                decCA = FS.FrameWork.Function.NConvert.ToDecimal(dtData.Compute("Sum(prepay_cost)", "pay_way = 'CA'"));
                this.fpSpread1_Sheet2.Cells[0, 1].Value = decCA;
                this.fpSpread1_Sheet2.Cells[0, 1].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                this.fpSpread1_Sheet2.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                decimal decCH = 0;
                decCH = FS.FrameWork.Function.NConvert.ToDecimal(dtData.Compute("Sum(prepay_cost)", "pay_way = 'CH'"));
                this.fpSpread1_Sheet2.Cells[0, 2].Value = decCH;
                this.fpSpread1_Sheet2.Cells[0, 2].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                this.fpSpread1_Sheet2.Cells[0, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                decimal decCD = 0;
                decCD = FS.FrameWork.Function.NConvert.ToDecimal(dtData.Compute("Sum(prepay_cost)", "pay_way = 'CD'"));
                this.fpSpread1_Sheet2.Cells[0, 3].Value = decCD;
                this.fpSpread1_Sheet2.Cells[0, 3].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                this.fpSpread1_Sheet2.Cells[0, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                decimal decOther = totPrepayCost - decCA - decCH - decCD;
                this.fpSpread1_Sheet2.Cells[0, 4].Value = decOther;
                this.fpSpread1_Sheet2.Cells[0, 4].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                this.fpSpread1_Sheet2.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            }

            ////预收收入计算
            //if (this.fpSpread1_Sheet1.ColumnCount > 0)
            //{
            //    decimal totPrepayCost = 0;
            //    for (int rowIndex = 0; rowIndex < this.fpSpread1_Sheet1.RowCount; rowIndex++)
            //    {
            //        totPrepayCost += FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[rowIndex, 0].Value);
            //    }

            //    this.fpSpread1_Sheet2.Cells[0, 0].Value = totPrepayCost;
            //    this.fpSpread1_Sheet2.Cells[0, 0].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            //    this.fpSpread1_Sheet2.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //}


            //单元格底部画线
            FarPoint.Win.LineBorder bottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, false, false, true);

            //预收收入底部
            for (int colIndex = 0; colIndex < this.fpSpread1_Sheet2.ColumnCount; colIndex++)
            {
                this.fpSpread1_Sheet2.Cells[0, colIndex].Border = bottomBorder;
            }

            //预收单号、手工预收单号、预收收据备注单元格合并
            this.fpSpread1_Sheet2.Cells[1, 0].ColumnSpan = 6;
            this.fpSpread1_Sheet2.Cells[2, 0].ColumnSpan = 6;
            this.fpSpread1_Sheet2.Cells[3, 0].ColumnSpan = 6;

            //预收单号
            if (dtData != null)
            {
                string strTemp = "";
                List<string> lstInvoice = new List<string>();
                foreach (DataRow dr in dtData.Rows)
                {
                    strTemp = dr["receipt_no"].ToString().Trim();

                    if (!lstInvoice.Contains(strTemp))
                    {
                        lstInvoice.Add(strTemp);
                    }
                }

                if (lstInvoice.Count > 0)
                {
                    lstInvoice.Sort();
                    this.fpSpread1_Sheet2.Cells[1, 0].Value = "预收单号：" + lstInvoice[0] + "  到  " + lstInvoice[lstInvoice.Count - 1] + "   共 " + lstInvoice.Count.ToString() + " 张";
                }
                else
                {
                    this.fpSpread1_Sheet2.Cells[1, 0].Value = "预收单号";
                }
            }
            else
            {
                this.fpSpread1_Sheet2.Cells[1, 0].Value = "预收单号";
            }
            this.fpSpread1_Sheet2.Cells[2, 0].Value = "手工预收单号";
            this.fpSpread1_Sheet2.Cells[3, 0].Value = "预收收据备注";
            this.fpSpread1_Sheet2.Cells[3, 0].Border = bottomBorder;


            //注销
            this.fpSpread1_Sheet2.Cells[4, 0].ColumnSpan = 6;
            this.fpSpread1_Sheet2.Cells[4, 0].Text = "注销";
            this.fpSpread1_Sheet2.Cells[4, 0].Border = bottomBorder;

            //作废
            this.fpSpread1_Sheet2.Cells[5, 0].ColumnSpan = 6;
            this.fpSpread1_Sheet2.Cells[5, 0].Text = "作废";
            this.fpSpread1_Sheet2.Cells[5, 0].Border = bottomBorder;
            if (dtData != null)
            {
                string strTemp = "";
                string strInvoiceNO = "";
                List<string> lstInvoice = new List<string>();
                foreach (DataRow dr in dtData.Rows)
                {
                    strTemp = dr["prepay_state"].ToString().Trim();

                    if (strTemp == "1")
                    {
                        strTemp = dr["receipt_no"].ToString().Trim();

                        if (!lstInvoice.Contains(strTemp))
                        {
                            lstInvoice.Add(strTemp);

                            strInvoiceNO += strTemp + "、";
                        }
                    }
                }

                if (!string.IsNullOrEmpty(strInvoiceNO))
                {
                    strInvoiceNO = strInvoiceNO.Trim(new char[] { '、' });
                }

                this.fpSpread1_Sheet2.Cells[5, 0].Text = "作废：" + strInvoiceNO;
                
            }


            //外币明细
            this.fpSpread1_Sheet2.Cells[6, 0].ColumnSpan = 6;
            this.fpSpread1_Sheet2.Cells[6, 0].Text = "外币明细:";
            this.fpSpread1_Sheet2.Cells[6, 0].Border = bottomBorder;

            //复核 出纳
            this.fpSpread1_Sheet2.Cells[7, 0].ColumnSpan = 6;
            this.fpSpread1_Sheet2.Cells[7, 0].Text = "复核：                     出纳：                       ";
            this.fpSpread1_Sheet2.Cells[7, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpSpread1_Sheet2.Cells[7, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Bottom;
        }

        void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.SetTime();

        }

        void ucDailyReportPrepay_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            this.GetStaticTimeSQL = "select to_char(a.begin_date, 'yyyy-mm-dd hh24:mi:ss') || ' 到 ' || to_char(a.end_date, 'yyyy-mm-dd hh24:mi:ss') from zc_fin_ipb_daybalance a where a.oper_code = '{0}' and a.oper_date >= sysdate - {1} ORDER BY a.END_DATE DESC ";

            this.SetTime();
            this.LeftAdditionTitle = "";
            this.RightAdditionTitle = "";
            this.MainTitle = "收款员预收报表";
            this.MidAdditionTitle = "";
            this.IsDeptAsCondition = false;
            this.SQLIndexs = "SOC.Fee.Inpatient.DailyReport.Prepay";
            this.RowHeaderVisible = false;
            this.DetailRowHeaderVisible = false;
            
            this.ColumnHeaderHeight = 24f;
            this.fpSpread1_Sheet1.ColumnHeader.Rows[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.QueryDataWhenInit = false;

            this.fpSpread1_Sheet2.Rows.Default.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.lbAdditionTitleMid.Font = new Font(this.lbAdditionTitleMid.Font.FontFamily, this.lbAdditionTitleMid.Font.Size, FontStyle.Regular);

            this.Init();
            
            this.QueryData();
        }

        private void ucDailyReportPrepay_DelegateOperateionStart(string operType)
        {
            if (operType == "query")
            {
                this.lbAdditionTitleMid.Text = "收款员：" + FS.FrameWork.Management.Connection.Operator.Name + "(" + FS.FrameWork.Management.Connection.Operator.ID + ")   报表时间："
                    + this.dtStart.Value.ToString()
                    + " 到 "
                    + this.dtEnd.Value.ToString();
            }
        }

        private void ucDailyReportPrepay_DelegateOperateionEnd(string operType)
        {
            bool isManager = ((FS.HISFC.Models.Base.Employee)(FS.FrameWork.Management.Connection.Operator)).IsManager;
           
            if (!this.fpSpread1.Sheets.Contains(this.fpSpread1_Sheet2))
            {
                this.fpSpread1.Sheets.Add(this.fpSpread1_Sheet2);
            } 
            if(this.fpSpread1.Sheets.Contains(this.fpSpread1_Sheet1) && !isManager)
            {
                this.fpSpread1.Sheets.Remove(this.fpSpread1_Sheet1);
            }
            this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet2;

            this.SetReportValue();

            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet2, this.DetailSettingFilePatch);

            float totWith = 0;
            for (int colIndex = 0; colIndex < this.fpSpread1_Sheet2.ColumnCount; colIndex++)
            {
                if (this.fpSpread1_Sheet2.Columns[colIndex].Visible)
                {
                    totWith += this.fpSpread1_Sheet2.Columns[colIndex].Width;
                }
            }
            this.neuPanel1.Height = 1;
            this.neuPanel2.Width = (int)totWith;
        }
    }
}
