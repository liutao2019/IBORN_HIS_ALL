using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.InpatientFee.GuangZhou.GYZL.Report
{
    public partial class ucInpatientFeeReport : FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucInpatientFeeReport()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 常数业务层
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constantManager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 费用明细帐工具
        /// </summary>
        private InpatientFeeReportUtil feeUtil = new InpatientFeeReportUtil();

        /// <summary>
        /// 表格边框
        /// </summary>
        private FarPoint.Win.ComplexBorderSide cbs = new FarPoint.Win.ComplexBorderSide();

        /// <summary>
        /// 字体
        /// </summary>
        private Font font = new Font("宋体", 10F);
        #endregion

        #region 方法

        #region 查询相关

        private void queryFee(DateTime start, DateTime end, string patientNo)
        {
            clear();
            addTitle();
            ArrayList alInpatientNo = feeUtil.QueryInpatientNo(patientNo);
            foreach (string inpatientNo in alInpatientNo)
            {
                List<InpatientFee> inpatientFeeList = feeUtil.QueryInpatientFee(inpatientNo, start, end);
                InpatientInfo inpatientInfo = feeUtil.GetInpatientInfo(inpatientNo);
                addInpatientInfo(inpatientInfo);
                addHead();
                addFee(start, end, inpatientFeeList);
            }

            setPerfectSize();
        }

        #endregion

        #region 表格相关

        private void addTitle()
        {
            int curRow = this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Cells[curRow, 0].ColumnSpan = 4;
            this.neuSpread1_Sheet1.Cells[curRow, 0].Text = "在院患者医疗费用明细帐";
            this.neuSpread1_Sheet1.Cells[curRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[curRow, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[curRow, 0].Font = new Font("宋体", 14F, FontStyle.Bold);
        }

        private void clear()
        {
            this.neuSpread1_Sheet1.RowCount = 0;
        }

        private void addInpatientInfo(InpatientInfo inpatientInfo)
        {
            int curRow = this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Cells[curRow, 0].Text = "住院  " + inpatientInfo.PatientNo;
            this.neuSpread1_Sheet1.Cells[curRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Cells[curRow, 0].Font = font;
            this.neuSpread1_Sheet1.Cells[curRow, 1].Text = "姓  " + inpatientInfo.InPatientName;
            this.neuSpread1_Sheet1.Cells[curRow, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Cells[curRow, 1].Font = font;
            this.neuSpread1_Sheet1.Cells[curRow, 2].Text = "床  " + inpatientInfo.BedNo;
            this.neuSpread1_Sheet1.Cells[curRow, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Cells[curRow, 2].Font = font;
        }

        private void addHead()
        {
            int curRow = this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Rows[curRow].Border = new FarPoint.Win.ComplexBorder(cbs, cbs, cbs, cbs);
            this.neuSpread1_Sheet1.Cells[curRow, 0].Text = "日期";
            this.neuSpread1_Sheet1.Cells[curRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[curRow, 0].Font = font;
            this.neuSpread1_Sheet1.Cells[curRow, 1].Text = "应收";
            this.neuSpread1_Sheet1.Cells[curRow, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[curRow, 1].Font = font;
            this.neuSpread1_Sheet1.Cells[curRow, 2].Text = "已结算";
            this.neuSpread1_Sheet1.Cells[curRow, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[curRow, 2].Font = font;
            this.neuSpread1_Sheet1.Cells[curRow, 3].Text = "未结算";
            this.neuSpread1_Sheet1.Cells[curRow, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[curRow, 3].Font = font;
        }

        public void addFee(DateTime start, DateTime end, List<InpatientFee> inpatientFeeList)
        {
            //期初
            int curRow = this.neuSpread1_Sheet1.RowCount++;
            IEnumerable feeQuery = from inpatientFee in inpatientFeeList
                                   where inpatientFee.Type == FeeType.期初
                                   select inpatientFee;
            bool isHaveFee = false;
            foreach (InpatientFee inpatientFee in feeQuery)
            {
                isHaveFee = true;
                this.neuSpread1_Sheet1.Rows[curRow].Border = new FarPoint.Win.ComplexBorder(cbs, cbs, cbs, cbs);
                this.neuSpread1_Sheet1.Cells[curRow, 0].Text = "期初余额";
                this.neuSpread1_Sheet1.Cells[curRow, 0].Font = font;
                this.neuSpread1_Sheet1.Cells[curRow, 3].Text = inpatientFee.Fee.ToString("F2");
                this.neuSpread1_Sheet1.Cells[curRow, 3].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                this.neuSpread1_Sheet1.Cells[curRow, 3].Font = font;
            }
            if (!isHaveFee)
            {
                this.neuSpread1_Sheet1.Rows[curRow].Border = new FarPoint.Win.ComplexBorder(cbs, cbs, cbs, cbs);
                this.neuSpread1_Sheet1.Cells[curRow, 0].Text = "期初余额";
                this.neuSpread1_Sheet1.Cells[curRow, 0].Font = font;
                this.neuSpread1_Sheet1.Cells[curRow, 3].Text = "0";
                this.neuSpread1_Sheet1.Cells[curRow, 3].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                this.neuSpread1_Sheet1.Cells[curRow, 3].Font = font;
            }

            //按start-end赋值表格
            int dateCount = (end - start).Days + 1;
            for (int i = 0; i < dateCount; i++)
            {
                curRow = this.neuSpread1_Sheet1.RowCount++;
                string curDate = start.AddDays(i).ToString("yyyy-MM-dd");
                this.neuSpread1_Sheet1.Rows[curRow].Border = new FarPoint.Win.ComplexBorder(cbs, cbs, cbs, cbs);
                this.neuSpread1_Sheet1.Rows[curRow].Tag = start.AddDays(i).ToString("yyyy-MM-dd");
                this.neuSpread1_Sheet1.Cells[curRow, 0].Text = start.AddDays(i).ToString("yyyy-MM-dd");
                this.neuSpread1_Sheet1.Cells[curRow, 0].Font = font;
                this.neuSpread1_Sheet1.Cells[curRow, 1].Text = "0";
                this.neuSpread1_Sheet1.Cells[curRow, 1].Font = font;
                this.neuSpread1_Sheet1.Cells[curRow, 1].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                this.neuSpread1_Sheet1.Cells[curRow, 2].Text = "0";
                this.neuSpread1_Sheet1.Cells[curRow, 2].Font = font;
                this.neuSpread1_Sheet1.Cells[curRow, 2].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                this.neuSpread1_Sheet1.Cells[curRow, 3].Text = "0";
                this.neuSpread1_Sheet1.Cells[curRow, 3].Font = font;
                this.neuSpread1_Sheet1.Cells[curRow, 3].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                //应收、已结
                IEnumerable cfeeQuery = from inpatientFee in inpatientFeeList
                                        where inpatientFee.FeeDate.Equals(curDate)
                                        select inpatientFee;
                foreach (InpatientFee inpatientFee in cfeeQuery)
                {
                    if (inpatientFee.Type == FeeType.应收)
                    {
                        this.neuSpread1_Sheet1.Cells[curRow, 1].Text = inpatientFee.Fee.ToString("F2");
                    }
                    else if (inpatientFee.Type == FeeType.已结)
                    {
                        this.neuSpread1_Sheet1.Cells[curRow, 2].Text = inpatientFee.Fee.ToString("F2");
                    }
                }
                //未结算
                decimal tot = Decimal.Parse(this.neuSpread1_Sheet1.Cells[curRow - 1, 3].Text)
                    + Decimal.Parse(this.neuSpread1_Sheet1.Cells[curRow, 1].Text)
                    - Decimal.Parse(this.neuSpread1_Sheet1.Cells[curRow, 2].Text);
                if (tot == 0)
                {
                    this.neuSpread1_Sheet1.RowCount--;
                    if (i == dateCount - 1)
                    {
                        //最后一天判断是否有未结算,若为0则清除患者信息
                        this.neuSpread1_Sheet1.RowCount -= 3;
                    }
                    continue;
                }
                this.neuSpread1_Sheet1.Cells[curRow, 3].Text = tot.ToString("F2");

            }
        }

        private void setPerfectSize()
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                this.neuSpread1_Sheet1.Rows[i].Height = this.neuSpread1_Sheet1.Rows[i].GetPreferredHeight() + 20;
                this.neuSpread1_Sheet1.Rows[i].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }
        }

        private void resetSize(ref int width, ref int height)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.ColumnCount; i++)
            {
                width += (int)this.neuSpread1_Sheet1.Columns[i].Width;
            }
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                height += (int)this.neuSpread1_Sheet1.Rows[i].Height;
            }
        }

        #endregion

        #region 初始化

        private void initDateTime()
        {
            DateTime curTime = feeUtil.GetDateTimeFromSysDateTime().AddDays(-1);
            dtpStart.Value = new DateTime(curTime.Year, curTime.Month, curTime.Day, 0, 0, 0);
            dtpEnd.Value = new DateTime(curTime.Year, curTime.Month, curTime.Day, 23, 59, 59);
            dtpStart.MaxDate = new DateTime(curTime.Year, curTime.Month, curTime.Day, 0, 0, 0);
            dtpEnd.MaxDate = new DateTime(curTime.Year, curTime.Month, curTime.Day, 23, 59, 59);
        }

        #endregion

        #region 打印相关

        private void printData()
        {
            FS.FrameWork.WinForms.Classes.Print printer = new FS.FrameWork.WinForms.Classes.Print();
            this.neuPanel1.Visible = false;
            int width = 0;
            int height = 0;
            resetSize(ref width, ref height);
            printer.SetPageSize(new FS.HISFC.Models.Base.PageSize("InpatientFeeReport", width + 20, height + 20));
            if (((FS.HISFC.Models.Base.Employee)this.constantManager.Operator).IsManager)
            {
                printer.PrintPreview(5, 5, this);
            }
            else
            {
                printer.PrintPage(5, 5, this);
            }
            this.neuPanel1.Visible = true;
        }

        #endregion

        #endregion

        #region 事件

        protected override int OnQuery(object sender, object neuObject)
        {
            DateTime start = new DateTime(dtpStart.Value.Year, dtpStart.Value.Month, dtpStart.Value.Day, 0, 0, 0);
            DateTime end = new DateTime(dtpEnd.Value.Year, dtpEnd.Value.Month, dtpEnd.Value.Day, 23, 59, 59);
            string patientNo = txtPatientNo.Text;
            queryFee(start, end, patientNo);
            return 0;
        }

        protected override void OnLoad(EventArgs e)
        {
            initDateTime();
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            printData();
            return 0;
        }
        #endregion
    }
}
