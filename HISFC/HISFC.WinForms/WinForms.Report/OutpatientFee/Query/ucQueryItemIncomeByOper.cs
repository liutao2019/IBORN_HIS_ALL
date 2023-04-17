using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.OutpatientFee.Query
{
    public partial class ucQueryItemIncomeByOper : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucQueryItemIncomeByOper()
        {
            InitializeComponent();
        }

        #region 变量

        FS.HISFC.BizLogic.Fee.FeeReport feeMgr = new FS.HISFC.BizLogic.Fee.FeeReport();

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
        [Description("报表类型:1收费员汇总报表(按项目);2收费员汇总报表(按支付方式);3挂号员汇总报表"), Category("数据")]
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

        private string filePath = string.Empty;

        #endregion

        #region  方法

        public int QueryReport()
        {
            //清空,必须要清空(因为有些行是特定设置的。如果直接将DataSource的值变了，但是neuspread_sheet已有的行的格式是不变，所以一般情况下要将sheet清空)
            this.neuSpread1_Sheet1.Rows.Count = 0;

            string begin = this.beginDate.Value.ToString();
            string end = this.endDate.Value.ToString();
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

            this.ShowData(dsResult);

            //this.neuSpread1_Sheet1.DataSource = dsResult.Tables[0];
            //this.neuSpread1_Sheet1.DataAutoSizeColumns = false;
            //for (int j = 0; j < this.neuSpread1_Sheet1.Rows.Count; j++)
            //{
            //    this.neuSpread1_Sheet1.Columns[j].Width = this.neuSpread1_Sheet1.Columns[j].GetPreferredWidth();
            //}

            #region  设置表头位置

            this.lblTitle.Text = this.titleName;

            this.lblInfo.Text = "打印人：" + this.feeMgr.Operator.Name + "     时间范围：" + this.beginDate.Value.ToString() + " 至 " + this.endDate.Value.ToString();

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
                    this.neuSpread1_Sheet1.Cells[index, 0].CellType = txtTppe;
                    this.neuSpread1_Sheet1.Cells[index, 0].Text = dr[0].ToString().PadLeft(6, '0');
                    this.neuSpread1_Sheet1.Cells[index, 1].Text = dr[1].ToString();
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

                    this.neuSpread1_Sheet1.Cells[index, 12].Text = dr[12].ToString(); //本院职工
                    this.neuSpread1_Sheet1.Cells[index, 13].Text = dr[13].ToString();//日结次数

                    index++;
                }
            }
            else if (this.reportType == "3")
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

                    this.neuSpread1_Sheet1.Cells[index, 8].Text = dr[8].ToString(); //日结次数

                    index++;
                }

            }
        }


        /// <summary>
        /// 增加汇总行：查询的时候，合并；导出的时候，不合并【合并对导出有问题】
        /// </summary>
        /// <returns></returns>
        public void AddSumRow(bool isColumnSpan)
        {
            int index = this.neuSpread1_Sheet1.Rows.Count;
            this.neuSpread1_Sheet1.Rows.Add(index, 1);

            if (isColumnSpan)
            {
                this.neuSpread1_Sheet1.Cells[index, 0].ColumnSpan = 2;
            }
            this.neuSpread1_Sheet1.Cells[index, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[index, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[index, 0].Text = "合计";
            for (int i = 2; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                decimal totCost = 0m;
                for (int j = 0; j < this.neuSpread1_Sheet1.Rows.Count - 1; j++)
                {
                    totCost += FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[j, i].Text);
                }

                if ((this.reportType == "2" || this.reportType == "3") && (i == 2))
                {
                    this.neuSpread1_Sheet1.Cells[index, i].Text = totCost.ToString();
                    continue ; 
                }

                FarPoint.Win.Spread.CellType.NumberCellType cellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                this.neuSpread1_Sheet1.Cells[index, i].CellType = cellType;
                this.neuSpread1_Sheet1.Cells[index, i].Text = totCost.ToString();
            }

            if (this.reportType == "2")
            {
                index = this.neuSpread1_Sheet1.Rows.Count;
                this.neuSpread1_Sheet1.Rows.Add(index, 1);
                this.neuSpread1_Sheet1.Cells[index, 0].Text = "现金";

                //现金金额
                string caValue = this.neuSpread1_Sheet1.Cells[index - 1, 4].Text;

                if (isColumnSpan)
                {
                    this.neuSpread1_Sheet1.Cells[index, 1].ColumnSpan = 2;
                }
                this.neuSpread1_Sheet1.Cells[index, 1].Text = caValue;
                this.neuSpread1_Sheet1.Cells[index, 3].Text = "大写";

                if (isColumnSpan)
                {
                    this.neuSpread1_Sheet1.Cells[index, 4].ColumnSpan = 9;
                }

                this.neuSpread1_Sheet1.Cells[index, 4].Text = FS.FrameWork.Public.String.LowerMoneyToUpper(FS.FrameWork.Function.NConvert.ToDecimal(caValue));

                //FarPoint.Win.LineBorder line = new FarPoint.Win.LineBorder(System.Drawing.Color.White, 0, true, true, true, true);
                //增加缴款人和收款人字段
                //index = this.neuSpread1_Sheet1.Rows.Count;
                //this.neuSpread1_Sheet1.Rows.Add(index, 1);
                //this.neuSpread1_Sheet1.Rows[index].Border = line;
                //this.neuSpread1_Sheet1.Cells[index, 0].Text = "缴款人：";
                //this.neuSpread1_Sheet1.Cells[index, 1].ColumnSpan = 2;

                //this.neuSpread1_Sheet1.Cells[index, 3].Text = "收款人：";
                //this.neuSpread1_Sheet1.Cells[index, 4].ColumnSpan = 2;
            }

            if (this.reportType == "3")
            {
                index = this.neuSpread1_Sheet1.Rows.Count;
                this.neuSpread1_Sheet1.Rows.Add(index, 1);
                this.neuSpread1_Sheet1.Cells[index, 0].Text = "现金";

                string caValue = this.neuSpread1_Sheet1.Cells[index - 1, 3].Text;

                if (isColumnSpan)
                {
                    this.neuSpread1_Sheet1.Cells[index, 1].ColumnSpan = 2;
                }

                this.neuSpread1_Sheet1.Cells[index, 1].Text = caValue;
                this.neuSpread1_Sheet1.Cells[index, 3].Text = "大写";

                if (isColumnSpan)
                {
                    this.neuSpread1_Sheet1.Cells[index, 4].ColumnSpan = 5;
                }
                this.neuSpread1_Sheet1.Cells[index, 4].Text = FS.FrameWork.Public.String.LowerMoneyToUpper(FS.FrameWork.Function.NConvert.ToDecimal(caValue));

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

            //收费员汇总报表(按项目)
            if (this.ReportType == "1")
            {
                //先清空
                this.neuSpread1_Sheet1.Rows.Count = 0;
                this.neuSpread1_Sheet1.Columns.Count = 0;

                this.neuSpread1_Sheet1.Columns.Count = 15;

                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].Text = "工号";
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].Text = "姓名";
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].Text = "总金额";

                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 3].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 3].Text = "西药费";
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 4].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 4].Text = "中成药";
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].Text = "中草药";

                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 6].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 6].Text = "化验费";
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 7].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 7].Text = "检查费";
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 8].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 8].Text = "诊察费";

                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 9].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 9].Text = "材料费";
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 10].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 10].Text = "手术费";
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 11].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 11].Text = "诊疗费";

                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 12].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 12].Text = "床位费";
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 13].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 13].Text = "挂号费";
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 14].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 14].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 14].Text = "其他费";

                this.filePath = Application.StartupPath + @".\profile\operItemIncome.xml";
                if (System.IO.File.Exists(filePath))
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.neuSpread1_Sheet1, this.filePath);
                }
                else
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.filePath);
                }
            }
            else if(this.ReportType == "2")
            {

                //先清空
                this.neuSpread1_Sheet1.Rows.Count = 0;
                this.neuSpread1_Sheet1.Columns.Count = 0;

                this.neuSpread1_Sheet1.Columns.Count = 14;

                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].Text = "工号";
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].Text = "姓名";
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].Text = "发票数";

                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 3].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 3].Text = "总金额";
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 4].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 4].Text = "现金";
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].Text = "信用卡";

                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 6].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 6].Text = "金卡";
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 7].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 7].Text = "IC卡";
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 8].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 8].Text = "社区转诊";

                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 9].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 9].Text = "特定门诊转诊";
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 10].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 10].Text = "特定门诊无卡";
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 11].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 11].Text = "公务员体检";

                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 12].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 12].Text = "职工及家属";

                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 13].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 13].Text = "日结次数";

                this.filePath = Application.StartupPath + @".\profile\operIncomePayMode.xml";
                if (System.IO.File.Exists(filePath))
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.neuSpread1_Sheet1, this.filePath);
                }
                else
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.filePath);
                }

                //打印页脚
                this.neuPanel2.Visible = true;
            }
            else if (this.reportType == "3")
            {
                //挂号员汇总报表
                //先清空
                this.neuSpread1_Sheet1.Rows.Count = 0;
                this.neuSpread1_Sheet1.Columns.Count = 0;
                this.neuSpread1_Sheet1.Columns.Count = 9;

                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].Text = "工号";
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].Text = "姓名";
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].Text = "发票数";

                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 3].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 3].Text = "总金额";
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 4].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 4].Text = "挂号费";
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].Text = "卡工本费";

                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 6].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 6].Text = "病历本费";
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 7].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 7].Text = "诊查费";

                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 8].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 8].Text = "日结次数";

                this.filePath = Application.StartupPath + @".\profile\operRegisterIncome.xml";
                if (System.IO.File.Exists(filePath))
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.neuSpread1_Sheet1, this.filePath);
                }
                else
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.filePath);
                }

                //打印页脚
                this.neuPanel2.Visible = true;
            }
            
            base.OnLoad(e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryReport();
            this.AddSumRow(true);
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
            ps = pgMgr.GetPageSize("ClinicFeedetail");

            if (ps == null)
            {
                //默认为A4纸张
                ps = new FS.HISFC.Models.Base.PageSize("A4", 827, 1169);
            }
            
            print.SetPageSize(ps);

            int fromPage = 1;
            int toPage = (System.Int32)Math.Ceiling((double)this.neuSpread1_Sheet1.Rows.Count / lineCount);
            this.neuPrint.Dock = DockStyle.None;
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

                //this.neuPrint.Width = 800;
                //int a = this.neuPrint.Width;
                //int b = this.neuPrint.Height;
                print.PrintPage(0, 0, this.neuPrint);
            }
            this.neuPrint.Dock = DockStyle.Fill;

            //打印完之后全部显示
            for (int k = 0; k < this.neuSpread1_Sheet1.Rows.Count; k++)
            {
                this.neuSpread1_Sheet1.Rows[k].Visible = true;
            }


            return base.OnPrint(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            try
            {
                //导出的时候不合并
                this.QueryReport();
                this.AddSumRow(false);

                string fileName = "";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Microsoft Excel 工作薄 (*.xls)|*.*";
                DialogResult result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    fileName = dlg.FileName;
                    this.neuSpread1.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
                }
            }
            catch (Exception ex)
            {
                //查询的时候不合并
                this.QueryReport();
                this.AddSumRow(true);
                MessageBox.Show("导出数据发生错误>>" + ex.Message);
                return -1;
            }

            //查询的时候不合并
            this.QueryReport();
            this.AddSumRow(true);
            return 0;
        }
        

        #endregion

        private void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.filePath);
        }

    }
}
