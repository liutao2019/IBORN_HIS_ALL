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
    public partial class ucQueryBalanceIncomeByDept : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucQueryBalanceIncomeByDept()
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

            string begin = this.beginDate.Value.Date.ToString();
            string end = this.endDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59).ToString();

            string strDeptID = "";
            if (pnlDept.Visible && this.cmbDept.SelectedItem != null)
            {
                strDeptID = this.cmbDept.SelectedItem.ID;
            }
            else
            {
                strDeptID = "ALL";
            }

            DataSet dsResult = new DataSet();
            if (this.feeMgr.GYZLQueryItemIncomeByDept(this.sqlID, this.StatClass,strDeptID, begin, end, ref dsResult) == -1)
            {
                MessageBox.Show(this.feeMgr.Err);
                return -1;
            }

            if (dsResult.Tables[0].Rows.Count <= 0)
            {
                MessageBox.Show("该段时间内没有数据!");
                return -1;
            }
            //this.ShowData(dsResult);

            //this.neuSpread1_Sheet1.DataSource = dsResult.Tables[0];
            //this.neuSpread1_Sheet1.DataAutoSizeColumns = false;
            //for (int j = 0; j < this.neuSpread1_Sheet1.Rows.Count; j++)
            //{
            //    this.neuSpread1_Sheet1.Columns[j].Width = this.neuSpread1_Sheet1.Columns[j].GetPreferredWidth();
            //}

            if(dsFeeStat.Tables[0].Rows.Count<=0)
            {
                 return -1;
            }
            SetReportColumnHeader(dsFeeStat.Tables[0]);
            int autoColCount = dsFeeStat.Tables[0].Rows.Count-3;
            SetReportData(dsResult.Tables[0], autoColCount);

            #region  设置表头位置

            this.lblTitle.Text = this.titleName;

            this.lblInfo.Text = "开始时间：" + this.beginDate.Value.ToShortDateString() + " 截止时间：" + this.endDate.Value.ToShortDateString();//"打印人：" + this.feeMgr.Operator.Name + 

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
            iniColumnDdata();
            #region 貌似没有用哦
            this.filePath = Application.StartupPath + @".\profile\GYZLQueryMZIncomeByDept.xml";
            if (System.IO.File.Exists(filePath))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.neuSpread1_Sheet1, this.filePath);
            }
            else
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.filePath);
            }

            //return;

            ////收费员汇总报表(按项目)
            //if (this.ReportType == "1")
            //{
            //    //先清空
            //    this.neuSpread1_Sheet1.Rows.Count = 0;
            //    this.neuSpread1_Sheet1.Columns.Count = 0;

            //    this.neuSpread1_Sheet1.Columns.Count = 15;

            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].Text = "工号";
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].Text = "姓名";
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].Text = "总金额";

            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 3].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 3].Text = "西药费";
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 4].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 4].Text = "中成药";
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].Text = "中草药";

            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 6].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 6].Text = "化验费";
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 7].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 7].Text = "检查费";
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 8].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 8].Text = "诊察费";

            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 9].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 9].Text = "材料费";
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 10].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 10].Text = "手术费";
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 11].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 11].Text = "诊疗费";

            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 12].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 12].Text = "床位费";
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 13].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 13].Text = "挂号费";
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 14].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 14].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 14].Text = "其他费";

            //    this.filePath = Application.StartupPath + @".\profile\GYZLQueryMZIncomeByDept.xml";
            //    if (System.IO.File.Exists(filePath))
            //    {
            //        FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.neuSpread1_Sheet1, this.filePath);
            //    }
            //    else
            //    {
            //        FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.filePath);
            //    }
            //}
            //else if(this.ReportType == "2")
            //{

            //    //先清空
            //    this.neuSpread1_Sheet1.Rows.Count = 0;
            //    this.neuSpread1_Sheet1.Columns.Count = 0;

            //    #region 作废先
            //    /*this.neuSpread1_Sheet1.Columns.Count = 11;

            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].Text = "收费员";
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].Text = "发票起止号";
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].Text = "发票张数";

            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 3].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 3].Text = "合计";

            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 4].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 4].Text = "记账";
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].Text = "中行磁卡";
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 6].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 6].Text = "商行磁卡";

            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 7].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 7].Text = "现金";
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 8].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 8].Text = "汇款";
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 9].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 9].Text = "支票";

            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 10].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 10].Text = "优惠";
            //    */
            //    #endregion 

            //    this.neuSpread1_Sheet1.Columns.Count = 9;

            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].Text = "收费员";

            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].Text = "合计";

            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].Text = "记账";
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 3].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 3].Text = "中行磁卡";
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 4].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 4].Text = "商行磁卡";

            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].Text = "现金";
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 6].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 6].Text = "汇款";
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 7].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 7].Text = "支票";

            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 8].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 8].Text = "优惠";

            //    this.filePath = Application.StartupPath + @".\profile\gyzlOperBalanceIncomePayMode.xml";
            //    if (System.IO.File.Exists(filePath))
            //    {
            //        FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.neuSpread1_Sheet1, this.filePath);
            //    }
            //    else
            //    {
            //        FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.filePath);
            //    }
            //}
            #endregion
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
            this.neuSpread1.Export();
            return base.Export(sender, neuObject);
        }
        

        #endregion

        private void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.filePath);
        }


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


        private void SetReportColumnHeader(DataTable dt)
        {
            DataView dv = new DataView();
            dv = dt.DefaultView;
            if (dv == null || dv.Count <= 0) return;

            this.neuSpread1_Sheet1.RowCount = 0;
            dv.RowFilter = "";
            this.neuSpread1_Sheet1.ColumnCount = dv.Count + 4;

            dv.RowFilter = "fee_stat_name not in ('西药费','中成费','中草费')";
            //------------------head

            this.neuSpread1_Sheet1.ColumnHeader.RowCount = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].Text = "科室";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].ColumnSpan = 4;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].Text = "药品收入";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 1].Text = "西药费";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 1].Column.Tag = "西药费";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 2].Text = "中成费";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 2].Column.Tag = "中成费";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 3].Text = "中草费";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 3].Column.Tag = "中草费";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 3].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 4].Text = "合计";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 4].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].ColumnSpan = dv.Count + 1;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].Text = "医疗收入";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            for (int i = 0; i < dv.Count; i++)
            {
                this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 5 + i].Text = dv[i]["fee_stat_name"].ToString();
                this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 5 + i].Column.Tag = dv[i]["fee_stat_name"].ToString();
                this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 5 + i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 5 + i].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }

            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 5 + dv.Count].Text = "合计";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 5 + dv.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 5 + dv.Count].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 6 + dv.Count].Text = "金额合计";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 6 + dv.Count].RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 6 + dv.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 6 + dv.Count].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            //---------------------head      

        }

        private void SetReportData(DataTable table, int autoColCount)
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
            //-----------data start
            DataView dvData = table.DefaultView;
            string dataRowFilter = "fee_stat_name in ('西药费','中成费','中草费') and  dept_name = {0}";
            Hashtable hs = new Hashtable();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string strDptCode = table.Rows[i]["reg_dpcd"].ToString();
                string strDptName = table.Rows[i]["dept_name"].ToString();
                string strFeeStat = table.Rows[i]["fee_stat_name"].ToString();
                decimal strCost = FS.FrameWork.Function.NConvert.ToDecimal(table.Rows[i][4].ToString());

                if (hs.Contains(strDptName))
                {
                    FarPoint.Win.Spread.Column col = neuSpread1_Sheet1.GetColumnFromTag(null, strFeeStat);
                    neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, col.Index].Text = strCost.ToString("0.00");
                }
                else
                {
                    neuSpread1_Sheet1.Rows.Count += 1;
                    hs.Add(strDptName, "");
                    neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].Tag = strDptCode;
                    neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].Text = strDptName;
                    neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    FarPoint.Win.Spread.Column col = neuSpread1_Sheet1.GetColumnFromTag(null, strFeeStat);
                    neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, col.Index].Text = strCost.ToString("0.00");
                }
            }
            //----------data end
            FarPoint.Win.Spread.CellType.NumberCellType cellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            //填充0
            for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
            {
                for (int j = 1; j < neuSpread1_Sheet1.ColumnCount; j++)
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
            for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
            {
                neuSpread1_Sheet1.Cells[i, 4].Formula = string.Format("sum(B{0}:D{0})", (i + 1).ToString());

                neuSpread1_Sheet1.Cells[i, 5 + autoColCount].Formula = string.Format("sum(F{0}:{1}{0})", (i + 1).ToString(), (char)(69 + autoColCount));

                neuSpread1_Sheet1.Cells[i, 6 + autoColCount].Formula = string.Format("sum(E{0},{1}{0})", (i + 1).ToString(), (char)(70 + autoColCount));
            }

            //行合计
            neuSpread1_Sheet1.Rows.Count += 1;
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].Text = "合计:";
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            for (int i = 1; i < neuSpread1_Sheet1.ColumnCount; i++)
            {
                string strFormula = "sum(" + (char)(65 + i) + "1:"
                                           + (char)(65 + i) + (neuSpread1_Sheet1.RowCount - 1).ToString()
                                    + ")";
                neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, i].Formula = strFormula;
                neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, i].CellType = cellType;
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

            this.SetReportColumnHeader(dsFeeStat.Tables[0]);
        }


    }
}
