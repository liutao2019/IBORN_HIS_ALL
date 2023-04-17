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
    public partial class ucQueryPubReimbursementReport : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 构造函数

        public ucQueryPubReimbursementReport()
        {
            InitializeComponent();
        }

        #endregion

        #region 变量

        FS.HISFC.BizLogic.Fee.FeeReport feeMgr = new FS.HISFC.BizLogic.Fee.FeeReport();
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
        ArrayList alMinFee = new ArrayList();
        StringBuilder sb = new StringBuilder();

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

        #region  查询

        /// <summary>
        /// 报表查询
        /// </summary>
        /// <returns></returns>
        public int QueryReport()
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;

            #region  设置表头位置

            this.lblTitle.Text = this.titleName;

            this.lblInfo.Text = "医疗单位:(盖章) " + conMgr.GetHospitalName() + "     统计日期: " + this.beginDate.Value.ToString() + " 至 " + this.endDate.Value.ToString() + "     单位:元";

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

            return 1;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private int GetParams(ref string[] args)
        {
            if (this.checkBox1.Checked)
            {
                if (this.beginDate.Value > this.endDate.Value)
                {
                    MessageBox.Show("查询开始时间不能大于结束时间!", "提示");
                    return -1;
                }
                args[0] = this.beginDate.Value.ToString();
                args[1] = this.endDate.Value.ToString();
            }
            else
            {
                args[0] = "0001-01-01 00:00:00";
                args[1] = "9999-12-31 23:59:59";
            }

            if (this.checkBox2.Checked && this.cmbPatientType.Tag != null && this.cmbPatientType.Tag.ToString() != "")
            {

                args[2] = this.cmbPatientType.Tag.ToString();
            }
            else
            {
                this.cmbPatientType.Tag = "ALL";
                args[2] = sb.ToString();

            }

            return 0;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            //初始化查询时间
            DateTime now = feeMgr.GetDateTimeFromSysDateTime();
            this.beginDate.Value = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            this.endDate.Value = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);

            #region 权限控制作废先

            //// 当前登录员工
            //FS.HISFC.Models.Base.Employee employee = conMgr.Operator as FS.HISFC.Models.Base.Employee;
            ////医技科室
            //ArrayList arlTDept = this.managerIntegrate.GetDeptmentByType(FS.HISFC.Models.Base.EnumDepartmentType.F.ToString());
            //ArrayList arlTDeptList = new ArrayList();
            //switch (strPriv)
            //{
            //    case "0":
            //        FS.HISFC.Models.Base.Department dept = new FS.HISFC.Models.Base.Department();
            //        dept.ID = "ALL";
            //        dept.Name = "全部";
            //        arlTDeptList.Add(dept);

            //        arlTDeptList.AddRange(arlTDept);
            //        break;

            //    case "1":
            //    case "2":
            //        foreach (FS.HISFC.Models.Base.Department dept1 in arlTDept)
            //        {
            //            if (dept1.ID == employee.Dept.ID)
            //            {
            //                arlTDeptList.Add(dept1);
            //            }
            //        }

            //        break;

            //    default:
            //        break;
            //}
            ////this.cmbPatientType.AddItems(arlTDeptList);
            ////this.cmbPatientType.SelectedIndex = 0;

            //// 人员
            //ArrayList arlEmployee = this.managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.F);
            //ArrayList arlEmployeeList = new ArrayList();

            //switch (strPriv)
            //{
            //    case "0":
            //        FS.HISFC.Models.Base.Employee emp = new FS.HISFC.Models.Base.Employee();
            //        emp.ID = "ALL";
            //        emp.Name = "全部";
            //        arlEmployeeList.Add(emp);

            //        arlEmployeeList.AddRange(arlEmployee);
            //        break;

            //    case "1":
            //        foreach (FS.HISFC.Models.Base.Employee emp1 in arlEmployee)
            //        {
            //            if (emp1.Dept.ID == employee.Dept.ID)
            //            {
            //                arlEmployeeList.Add(emp1);
            //            }
            //        }

            //        break;

            //    case "2":
            //        foreach (FS.HISFC.Models.Base.Employee emp2 in arlEmployee)
            //        {
            //            if (emp2.ID == employee.ID)
            //            {
            //                arlEmployeeList.Add(emp2);
            //            }
            //        }

            //        break;

            //    default:
            //        break;
            //}

            #endregion

            //初始化报表头
            this.SetReportHeader(null);
        }

        /// <summary>
        /// 分别设置报表行、列标题
        /// </summary>
        /// <param name="dt"></param>
        private void SetReportHeader(DataTable dt)
        {
            #region Init

            //if (dt == null || dt.Rows.Count <= 0) return;
            //int autoRowCount = dt.Rows.Count;

            int autoRowCount = 4;
            //清空数据
            this.neuSpread1_Sheet1.RowCount = 0;
            //清空行头标题
            this.neuSpread1_Sheet1.RowHeader.Rows.Count = 0;
            //设置报表行数
            this.neuSpread1_Sheet1.RowHeader.Rows.Count = autoRowCount + 3;
            this.neuSpread1_Sheet1.Rows.Count = autoRowCount + 3;

            #endregion

            FarPoint.Win.LineBorder lineBorder1 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame);
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.SystemColors.Window, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Black, FarPoint.Win.Spread.GridLines.Horizontal, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, true, true, false);

            #region ColumnHeader

            this.neuSpread1_Sheet1.ColumnHeader.RowCount = 3;
            this.neuSpread1_Sheet1.ColumnHeader.Columns.Count = 14;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].RowSpan = 3;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].Text = "类  别";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].Column.Width = 80F;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].RowSpan = 3;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].Text = "门诊人数";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].Column.Width = 80F;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].ColumnSpan = 12;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].Text = "门诊部分";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 2].ColumnSpan = 4;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 2].Text = "药费";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 2].Column.Width = 100F;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 2].Text = "中药";
            //this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 2].Column.Tag = "中药";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 2].Column.Width = 100F;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 3].Text = "西药";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 3].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 3].Column.Width = 100F;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 4].Text = "30%药品";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 4].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 4].Column.Width = 100F;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 5].Text = "特批药品";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 5].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 5].Column.Width = 100F;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 6].RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 6].Text = "诊金";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 6].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 6].Column.Width = 100F;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 7].ColumnSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 7].Text = "检查费";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 7].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 7].Text = "一般";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 7].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 7].Column.Width = 100F;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 8].Text = "150以上";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 8].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[2, 8].Column.Width = 100F;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 9].RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 9].Text = "治疗费";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 9].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 9].Column.Width = 100F;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 10].RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 10].Text = "输血";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 10].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 10].Column.Width = 100F;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 11].RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 11].Text = "小计";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 11].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 11].Column.Width = 100F;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 12].RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 12].Text = "自负金额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 12].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 12].Column.Width = 100F;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 13].RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 13].Text = "实际金额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 13].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[1, 13].Column.Width = 100F;

            #endregion

            //#region RowHeader

            //this.neuSpread1_Sheet1.RowHeader.ColumnCount = 2;

            //this.neuSpread1_Sheet1.RowHeader.Cells[0, 0].ColumnSpan = 2;
            //this.neuSpread1_Sheet1.RowHeader.Cells[0, 0].Text = "干部医疗费";
            //this.neuSpread1_Sheet1.RowHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.RowHeader.Cells[0, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.RowHeader.Cells[0, 0].Column.Width = 140F;

            //this.neuSpread1_Sheet1.RowHeader.Cells[1, 0].RowSpan = autoRowCount;
            //this.neuSpread1_Sheet1.RowHeader.Cells[1, 0].Text = "其中";
            //this.neuSpread1_Sheet1.RowHeader.Cells[1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.RowHeader.Cells[1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.RowHeader.Cells[0, 0].Column.Width = 100F;

            //for (int i = 0; i < autoRowCount; i++)
            //{
            //    this.neuSpread1_Sheet1.RowHeader.Cells[i + 1, 1].Text = i.ToString();//更新字头位置
            //    this.neuSpread1_Sheet1.RowHeader.Cells[i + 1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //    this.neuSpread1_Sheet1.RowHeader.Cells[i + 1, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //}

            //this.neuSpread1_Sheet1.RowHeader.Cells[this.neuSpread1_Sheet1.Rows.Count - 2, 0].Text = "家属医疗费";
            //this.neuSpread1_Sheet1.RowHeader.Cells[this.neuSpread1_Sheet1.Rows.Count - 2, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.RowHeader.Cells[this.neuSpread1_Sheet1.Rows.Count - 2, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            //this.neuSpread1_Sheet1.RowHeader.Cells[this.neuSpread1_Sheet1.Rows.Count - 2, 1].Text = "02";//更新字头位置
            //this.neuSpread1_Sheet1.RowHeader.Cells[this.neuSpread1_Sheet1.Rows.Count - 2, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.RowHeader.Cells[this.neuSpread1_Sheet1.Rows.Count - 2, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            //this.neuSpread1_Sheet1.RowHeader.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].ColumnSpan = 2;
            //this.neuSpread1_Sheet1.RowHeader.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Text = "合计";
            //this.neuSpread1_Sheet1.RowHeader.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.RowHeader.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            //#endregion
        }

        /// <summary>
        /// 设置报表数据
        /// </summary>
        /// <param name="table"></param>
        private void SetReportData(DataTable table)
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
            //-----------data start
            DataView dvData = table.DefaultView;

            Hashtable hs = new Hashtable();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string strDptName = table.Rows[i]["dept_name"].ToString();
                string strFeeCode = table.Rows[i]["fee_code"].ToString();
                string strFeeName = table.Rows[i]["fee_name"].ToString();
                decimal strCost = FS.FrameWork.Function.NConvert.ToDecimal(table.Rows[i]["tot_cost"].ToString());

                if (hs.Contains(strDptName))
                {
                    FarPoint.Win.Spread.Column col = neuSpread1_Sheet1.GetColumnFromTag(null, strFeeName);
                    neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, col.Index].Text = (FS.FrameWork.Function.NConvert.ToDecimal(neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, col.Index].Text) + strCost).ToString();
                }
                else
                {
                    neuSpread1_Sheet1.Rows.Count += 1;
                    hs.Add(strDptName, "");
                    neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].Text = strDptName;
                    neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    FarPoint.Win.Spread.Column col = neuSpread1_Sheet1.GetColumnFromTag(null, strFeeName);
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

            //行合计
            neuSpread1_Sheet1.Rows.Count += 1;
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].Text = "合计:";
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            //添加合计行			
            char charSum = 'B';
            char charEnd = 'A';
            string strSum = charSum.ToString();
            string strEndCol = charSum.ToString();
            int intAcount = 0;

            for (int intcol = 1; intcol < this.neuSpread1_Sheet1.Columns.Count; intcol++)
            {

                strSum = NumbertoString(intcol + 1);
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, intcol].Formula = string.Format("SUM({2}{0}:{2}{1})", 1, this.neuSpread1_Sheet1.Rows.Count - 1, strSum);
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, intcol].CellType = cellType;
            }

            //添加合计列

            charSum = 'A';
            //this.neuSpread1_Sheet1.Columns.Add(this.neuSpread1_Sheet1.ColumnCount, 1);
            //this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, this.neuSpread1_Sheet1.ColumnCount - 1).Text = "合计";
            //this.neuSpread1_Sheet1.Columns.Get(this.neuSpread1_Sheet1.ColumnCount - 1).Width = 120F;

            strEndCol = NumbertoString(this.neuSpread1_Sheet1.Columns.Count - 1);
            for (int introw = 0; introw < this.neuSpread1_Sheet1.Rows.Count; ++introw)
            {

                this.neuSpread1_Sheet1.Cells[introw, this.neuSpread1_Sheet1.Columns.Count - 1].Formula = string.Format("SUM({0}{2}:{1}{2})", charSum.ToString(), strEndCol, introw + 1);
                this.neuSpread1_Sheet1.Cells[introw, this.neuSpread1_Sheet1.Columns.Count - 1].CellType = cellType;
            }
        }

        /// <summary>
        /// 显示数据
        /// </summary>
        /// <param name="dsResult"></param>
        private void ShowData(DataSet dsResult)
        {
            DataTable dt = dsResult.Tables[0];

            this.neuSpread1_Sheet1.Rows.Count = 0;
            int index = this.neuSpread1_Sheet1.Rows.Count;
            FarPoint.Win.Spread.CellType.NumberCellType numCellType = new FarPoint.Win.Spread.CellType.NumberCellType();

            FarPoint.Win.Spread.CellType.TextCellType txtTppe = new FarPoint.Win.Spread.CellType.TextCellType();

            foreach (DataRow dr in dt.Rows)
            {
                this.neuSpread1_Sheet1.Rows.Add(index, 1);

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

                this.neuSpread1_Sheet1.Cells[index, 1].CellType = numCellType;
                this.neuSpread1_Sheet1.Cells[index, 1].Text = dr[1].ToString();

                this.neuSpread1_Sheet1.Cells[index, 2].CellType = numCellType;
                this.neuSpread1_Sheet1.Cells[index, 2].Text = dr[2].ToString();

                this.neuSpread1_Sheet1.Cells[index, 3].CellType = numCellType;
                this.neuSpread1_Sheet1.Cells[index, 3].Text = dr[3].ToString();

                this.neuSpread1_Sheet1.Cells[index, 4].CellType = numCellType;
                this.neuSpread1_Sheet1.Cells[index, 4].Text = dr[4].ToString();

                this.neuSpread1_Sheet1.Cells[index, 5].CellType = numCellType;
                this.neuSpread1_Sheet1.Cells[index, 5].Text = dr[5].ToString();

                this.neuSpread1_Sheet1.Cells[index, 6].CellType = numCellType;
                this.neuSpread1_Sheet1.Cells[index, 6].Text = dr[6].ToString();

                this.neuSpread1_Sheet1.Cells[index, 7].CellType = numCellType;
                this.neuSpread1_Sheet1.Cells[index, 7].Text = dr[7].ToString();

                this.neuSpread1_Sheet1.Cells[index, 8].CellType = numCellType;
                this.neuSpread1_Sheet1.Cells[index, 8].Text = dr[8].ToString();

                index++;
            }
        }

        /// <summary>
        /// 合计行
        /// </summary>
        /// <returns></returns>
        private int AddSumRows()
        {
            //添加合计行			
            char charSum = 'B';
            char charEnd = 'A';
            string strSum = charSum.ToString();
            string strEndCol = charSum.ToString();
            int intAcount = 0;

            for (int intcol = 1; intcol < this.neuSpread1_Sheet1.Columns.Count; intcol++)
            {
                strSum = NumbertoString(intcol + 1);
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, intcol].Formula = 
                    string.Format("SUM({2}{0}:{2}{1})", 1, this.neuSpread1_Sheet1.Rows.Count - 1, strSum);
                //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, intcol].CellType = cellType;
           }
            return 1;
        }

        /// <summary>
        /// 根据列数获取对应的EXCEL的列标
        /// </summary>
        /// <param name="n">列数</param>
        /// <returns>对应的EXCEL的列标(如FA)</returns>
        private string NumbertoString(int n)
        {

            string s = "";
            int r = 0;
            while (n != 0)
            {
                r = n % 26;

                char ch = ' ';

                if (r == 0)

                    ch = 'Z';

                else
                    ch = (char)(r - 1 + 'A');

                s = ch.ToString() + s;

                if (s[0] == 'Z')

                    n = n / 26 - 1;

                else

                    n /= 26;

            }
            return s;
        } 

        #endregion

        #region 事件

        protected override void OnLoad(EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            base.tv = new TreeView();
            TreeNode root1 = new TreeNode("省公医", 0, 1);
            root1.Tag = "ShengGY";
            base.tv.Nodes.Add(root1);
            TreeNode root5 = new TreeNode("本院", 0, 1);
            root5.Tag = "ThisHos";
            base.tv.Nodes.Add(root5);
            //			TreeNode root4 = new TreeNode("特约单位",0,1);
            //			root4.Tag="TeYue";
            //			this.tvList.Nodes.Add(root4);
            TreeNode root2 = new TreeNode("市公医", 0, 1);
            root2.Tag = "ShiGY";
            this.tv.Nodes.Add(root2);
            TreeNode root3 = new TreeNode("区公医", 0, 1);
            root3.Tag = "QuGY";
            base.tv.Nodes.Add(root3);
            base.tv.ExpandAll();
            base.tv.Visible = true;
            //初始化
            Init();
            //保存报表格式
            this.filePath = Application.StartupPath + @".\profile\GYZLQueryPubReimbursementReport.xml";
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

        private void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.filePath);
        }

        #endregion
    }
}
