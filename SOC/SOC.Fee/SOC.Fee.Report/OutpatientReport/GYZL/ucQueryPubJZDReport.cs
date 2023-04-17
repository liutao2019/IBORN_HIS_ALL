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
    public partial class ucQueryPubJZDReport : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucQueryPubJZDReport()
        {
            InitializeComponent();
        }

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

        /// <summary>
        /// 报表是否包含员工条件
        /// </summary>
        [Category("报表设置"), Description("报表是否包含病人类别条件")]
        public bool BlnPatientTypeCondition
        {
            get { return pnlPatientType.Visible; }
            set { pnlPatientType.Visible = value; }
        }

        /// <summary>
        /// 报表是否包含科室条件
        /// </summary>
        [Category("报表设置"), Description("报表是否包含科室条件")]
        public bool BlnPatientNameCondition
        {
            get { return pnlPatientName.Visible; }
            set { pnlPatientName.Visible = value; }
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

            //ArrayList alPatientType = this.cmbPatientType.alItems;
            //string pact_head = "'00','01','02','03','70','71','72','73','75'";

            string[] args = new string[4] { "ALL", "ALL", sb.ToString() , "ALL" };
            this.GetParams(ref args);

            DataSet dsResult = new DataSet();
            //Clinic.Report.QueryPatientFeePubAllDetail
            if (this.feeMgr.GYZLQueryItemIncomeByDept(this.sqlID, args[0], args[1], args[2], args[3], ref dsResult) == -1)
            {
                MessageBox.Show(this.feeMgr.Err);
                return -1;
            }

            if (dsResult.Tables[0].Rows.Count <= 0)
            {
                MessageBox.Show("该段时间内没有数据!");
                return -1;
            }

            this.neuSpread1_Sheet1.DataSource = dsResult.Tables[0];
            this.neuSpread1_Sheet1.DataAutoSizeColumns = false;

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
            //医技科室
            ArrayList arlTDept = this.managerIntegrate.GetDeptmentByType(FS.HISFC.Models.Base.EnumDepartmentType.T.ToString());
            ArrayList arlTDeptList = new ArrayList();
            switch (strPriv)
            {
                case "0":
                    FS.HISFC.Models.Base.Department dept = new FS.HISFC.Models.Base.Department();
                    dept.ID = "ALL";
                    dept.Name = "全部";
                    arlTDeptList.Add(dept);

                    arlTDeptList.AddRange(arlTDept);
                    break;

                case "1":
                case "2":
                    foreach (FS.HISFC.Models.Base.Department dept1 in arlTDept)
                    {
                        if (dept1.ID == employee.Dept.ID)
                        {
                            arlTDeptList.Add(dept1);
                        }
                    }

                    break;

                default:
                    break;
            }
            //this.cmbPatientType.AddItems(arlTDeptList);
            //this.cmbPatientType.SelectedIndex = 0;

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
            //this.cmbFeeOper.AddItems(arlEmployeeList);
            //this.cmbFeeOper.SelectedIndex = 0;

            Init();

            this.filePath = Application.StartupPath + @".\profile\GYZLQueryClinicTDeptIncome.xml";
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

        private void SetReportColumnHeader(ArrayList alMinFee)
        {
            if (alMinFee == null || alMinFee.Count <= 0) return;

            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ColumnCount = alMinFee.Count + 2;


            //------------------head

            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].Text = "科室";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            for (int i = 0; i < alMinFee.Count; i++)
            {
                FS.HISFC.Models.Base.Const cons = (FS.HISFC.Models.Base.Const)alMinFee[i];
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, i+1].Text = cons.Name;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, i+1].Column.Tag = cons.Name; //cons.ID;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, i+1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, i+1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }

            //---------------------head  
            //列合计
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, neuSpread1_Sheet1.ColumnCount-1].Text = "合计";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, neuSpread1_Sheet1.ColumnCount - 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, neuSpread1_Sheet1.ColumnCount - 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
        }

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

                strSum = NumbertoString(intcol+1);
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
                //if (this.fpSpread1_Sheet1.Cells[introw, this.fpSpread1_Sheet1.Columns.Count - 1].Text == null
                //    || this.fpSpread1_Sheet1.Cells[introw, this.fpSpread1_Sheet1.Columns.Count - 1].Text == ""
                //    || this.fpSpread1_Sheet1.Cells[introw, this.fpSpread1_Sheet1.Columns.Count - 1].Text == "0.00")
                //{
                //    this.fpSpread1_Sheet1.Rows[introw].Visible = false;
                //}
            }
            //#region 隐藏没产生数据的列
            ////保存隐藏列号
            //string strCol = "";
            //for (int i = 0; i < this.fpSpread1_Sheet1.Columns.Count; i++)
            //{
            //    for (int j = 0; j < this.fpSpread1_Sheet1.RowCount; j++)
            //    {
            //        if (j >= this.fpSpread1_Sheet1.RowCount - 1)
            //        {
            //            strCol += i.ToString() + ",";
            //            this.fpSpread1_Sheet1.Columns[i].Width = 0;
            //            this.fpSpread1_Sheet1.Columns[i].Visible = false;
            //            break;
            //        }
            //        else
            //        {
            //            if (this.fpSpread1_Sheet1.Cells[j, i].Text == null || this.fpSpread1_Sheet1.Cells[j, i].Text == "" || this.fpSpread1_Sheet1.Cells[j, i].Text == "0")
            //            {
            //                continue;
            //            }
            //            else
            //            {
            //                break;
            //            }
            //        }
            //    }

            //}
            //#endregion

        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            //this.dateBegin.Value = this.bookingMgr.GetDateTimeFromSysDateTime().Date;
            //this.dateEnd.Value = this.dateBegin.Value;
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "ALL";
            obj.Name = "全部";
            ArrayList all = new ArrayList();
            all.Add(obj);

            //病人类别

            ArrayList al = null;
            al = this.feeMgr.GYZLQueryPubOutPatientInfo("Clinic.Report.QueryPatientTypeAllDetail");
            for (int i = 0; i < al.Count; i++)
            {
                sb.Append("'");
                sb.Append(((FS.FrameWork.Models.NeuObject)(al[i])).ID.ToString());
                sb.Append("'");
                if (i != al.Count - 1)
                {
                    sb.Append(",");
                }
            }
            if (al == null) al = new ArrayList();
            all.AddRange(al);
            this.cmbPatientType.AddItems(all);
            this.cmbPatientType.SelectedIndex = 0;

            //患者姓名            
            al = this.feeMgr.GYZLQueryPubOutPatientInfo("Clinic.Report.QueryPubOutPatientInfo");
            if (al == null) al = new ArrayList();
            all.AddRange(al);
            this.cmbPatientName.AddItems(all);
            this.cmbPatientName.SelectedIndex = 0;

            ////操作员
            //al = deptMgr.QueryEmployeeAll();
            //if (al == null) al = new ArrayList();

            //this.cmbOper.AddItems(al);
        }


        private int GetParams(ref string[] args)
        {
            //string[] args = new string[4] { "ALL", "ALL", "ALL", "ALL" };
            //this.checkBox1.Enabled = false;
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

            if (this.checkBox3.Checked && this.cmbPatientName.Tag != null && this.cmbPatientName.Tag.ToString() != "")
            {
                args[3] = this.cmbPatientName.Tag.ToString();
            }
            else
            {
                this.cmbPatientName.Tag = "ALL";
            }

            return 0;
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


    }
}
