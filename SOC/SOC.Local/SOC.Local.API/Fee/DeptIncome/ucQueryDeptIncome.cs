using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.API.Fee.DeptIncome
{
    public partial class ucQueryDeptIncome : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucQueryDeptIncome()
        {
            InitializeComponent();
        }

        #region 变量

        FS.HISFC.BizLogic.Fee.FeeReport feeMgr = new FS.HISFC.BizLogic.Fee.FeeReport();
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
        ArrayList alMinFee = new ArrayList();

        /// <summary>
        /// 每页打印行数
        /// </summary>
        private int colCount = 60;

        /// <summary>
        /// 每页打印行数
        /// </summary>
        [Description("每页打印行数"), Category("打印设置")]
        public int ColCount
        {
            get
            {
                return this.colCount;
            }
            set
            {
                this.colCount = value;
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

        /// <summary>
        /// toolBar
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        #endregion

        #region  方法

        public int QueryReport()
        {
            //清空,必须要清空(因为有些行是特定设置的。如果直接将DataSource的值变了，但是neuspread_sheet已有的行的格式是不变，所以一般情况下要将sheet清空)
            this.neuSpread1_Sheet1.Rows.Count = 0;

            string begin = this.beginDate.Value.ToString();
            string end = this.endDate.Value.ToString();

            string strDeptID = "";
            if (pnlDept.Visible && this.cmbDept.SelectedItem != null)
            {
                strDeptID = this.cmbDept.SelectedItem.ID;
            }   

            DataSet dsResult = new DataSet();
            
            if (this.feeMgr.GYZLQueryItemIncomeByDept(this.sqlID, "",strDeptID, begin, end, ref dsResult) == -1)
            {
                MessageBox.Show(this.feeMgr.Err);
                return -1;
            }

            SetHeader(this.alMinFee.Count);

            SetReportColumnHeader(this.alMinFee);

            SetReportData(dsResult.Tables[0]);

            for (int j = 1; j < this.neuSpread1_Sheet1.ColumnCount; j++)
            {
                this.neuSpread1_Sheet1.Columns[j].Width = this.neuSpread1_Sheet1.Columns[j].GetPreferredWidth();
            }

            if (dsResult.Tables[0].Rows.Count <= 0)
            {
                MessageBox.Show("该段时间内没有数据!");
                return -1;
            }

            //#region  设置表头位置

            //this.lblTitle.Text = this.titleName;

            //this.lblInfo.Text = "打印人：" + this.feeMgr.Operator.Name + "     时间范围：" + this.beginDate.Value.ToString() + " 至 " + this.endDate.Value.ToString();

            ////设置标题位置
            //decimal spreadWidt = 0m;
            //foreach (FarPoint.Win.Spread.Column fpColumn in this.neuSpread1_Sheet1.Columns)
            //{
            //    spreadWidt += (decimal)fpColumn.Width;
            //}

            //if (spreadWidt > this.neuPrint.Width)
            //{
            //    spreadWidt = this.neuPrint.Width;
            //}

            //spreadWidt = spreadWidt - this.lblTitle.Width;
            //int titleX = FS.FrameWork.Function.NConvert.ToInt32((spreadWidt / 2));
            //if (titleX <= 0)
            //{
            //    titleX = 1;
            //}

            //this.lblTitle.Location = new Point(titleX, this.lblTitle.Location.Y);

            //#endregion

            //this.AddSumRow();

            return 1;
        }

        private void SetHeader(int width)
        {
            
            this.neuSpread1_Sheet1.Rows.Count = 0;

            this.neuSpread1_Sheet1.RowCount+=2;
            
            this.neuSpread1_Sheet1.Cells[0, 0].Text = this.titleName;
            this.neuSpread1_Sheet1.Cells[0, 0].Font = new Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            ;
            this.neuSpread1_Sheet1.Rows[0].Height = 35;
            this.neuSpread1_Sheet1.Cells[0, 0].ForeColor = Color.Black;
            this.neuSpread1_Sheet1.Cells[0, 0].ColumnSpan = width + 2;
            this.neuSpread1_Sheet1.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            
            this.neuSpread1_Sheet1.Cells[1, 0].Text = "时间范围：" + this.beginDate.Value.ToString() + " 至 " + this.endDate.Value.ToString();
            this.neuSpread1_Sheet1.Cells[1, 0].Font = new Font("宋体", 12);
            this.neuSpread1_Sheet1.Cells[1, 0].ForeColor = Color.Black;
            this.neuSpread1_Sheet1.Cells[1, 0].ColumnSpan = width + 2;
            this.neuSpread1_Sheet1.Cells[1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
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

                this.neuSpread1_Sheet1.RowCount++;
                int index = this.neuSpread1_Sheet1.RowCount - 1;

                foreach (DataRow dr in dt.Rows)
                {
                    this.neuSpread1_Sheet1.Rows.Add(index, 1);

                    FarPoint.Win.Spread.CellType.NumberCellType cellType = new FarPoint.Win.Spread.CellType.NumberCellType();

                    FarPoint.Win.Spread.CellType.TextCellType txtTppe = new FarPoint.Win.Spread.CellType.TextCellType();
                    this.neuSpread1_Sheet1.Cells[index, 0].CellType = txtTppe;
                    this.neuSpread1_Sheet1.Cells[index, 0].Text = dr[0].ToString();
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

        #endregion

        #region 事件

        protected override void OnLoad(EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            DateTime now = feeMgr.GetDateTimeFromSysDateTime();
            this.beginDate.Value = now;
            this.endDate.Value = now.AddDays(1);

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
            this.cmbDept.AddItems(arlTDeptList);
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

        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryReport();
            return 1;
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
                ps = new FS.HISFC.Models.Base.PageSize();
            }
            
            print.SetPageSize(ps);

            //int fromPage = 1;
            //int toPage = (System.Int32)Math.Ceiling((double)this.neuSpread1_Sheet1.ColumnCount/colCount);

            //for (int i = fromPage; i <= toPage; i++)
            //{
            //    for (int j = 0; j < this.neuSpread1_Sheet1.ColumnCount; j++)
            //    {
            //        if (j >= (i - 1) * this.colCount && (j + 1) <= i * this.colCount)
            //        {
            //            this.neuSpread1_Sheet1.Columns[j].Visible = true;
            //        }
            //        else
            //        {
            //            this.neuSpread1_Sheet1.Columns[j].Visible = false;
            //        }
            //    }
            //    print.PrintPage(0, 0, this.neuPrint);
            //}

            ////打印完之后全部显示
            //for (int k = 0; k < this.neuSpread1_Sheet1.ColumnCount; k++)
            //{
            //    this.neuSpread1_Sheet1.Columns[k].Visible = true;
            //}

            print.PrintDocument.DefaultPageSettings.Landscape = true;
            print.PrintPage(10, 10, this.neuPrint);

            return 1;
        }

        public override int Export(object sender, object neuObject)
        {
            this.neuSpread1_Sheet1.Cells[0, 0].ColumnSpan = 1;
            this.neuSpread1_Sheet1.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Cells[1, 0].ColumnSpan = 1;
            this.neuSpread1_Sheet1.Cells[1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            
            this.neuSpread1.Export();

            this.neuSpread1_Sheet1.Cells[0, 0].ColumnSpan = this.alMinFee.Count + 2;
            this.neuSpread1_Sheet1.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[1, 0].ColumnSpan = this.alMinFee.Count + 2;
            this.neuSpread1_Sheet1.Cells[1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            return 1;
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("返回", "返回主报表", (int)FS.FrameWork.WinForms.Classes.EnumImageList.F返回, true, false, null);
            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "返回")
            {
                this.neuSpread1.ActiveSheetIndex = 0;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
        #endregion

        private void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.filePath);
        }

        private void SetReportColumnHeader(ArrayList alMinFee)
        {
            if (alMinFee == null || alMinFee.Count <= 0) return;

            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.ColumnCount = alMinFee.Count + 2;


            //------------------head

            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "科室";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            for (int i = 0; i < alMinFee.Count; i++)
            {
                FS.HISFC.Models.Base.Const cons = (FS.HISFC.Models.Base.Const)alMinFee[i];
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, i + 1].Text = cons.Name;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, i + 1].Column.Tag = cons.Name; //cons.ID;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, i + 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, i + 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }

            //---------------------head  
            //列合计
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, neuSpread1_Sheet1.ColumnCount - 1].Text = "合计";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, neuSpread1_Sheet1.ColumnCount - 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, neuSpread1_Sheet1.ColumnCount - 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            
        }

        private void SetReportData(DataTable table)
        {
            //-----------data start
            DataView dvData = table.DefaultView;

            Hashtable hs = new Hashtable();
            
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string strDptCode = table.Rows[i]["exec_dpcd"].ToString();
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
                    neuSpread1_Sheet1.Rows[neuSpread1_Sheet1.RowCount - 1].Tag = strDptCode;
                    neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    FarPoint.Win.Spread.Column col = neuSpread1_Sheet1.GetColumnFromTag(null, strFeeName);
                    //neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, col.Index].Text = strCost.ToString("0.00");
                    neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, col.Index].Text = (FS.FrameWork.Function.NConvert.ToDecimal(neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, col.Index].Text) + strCost).ToString();
                }
            }
            //----------data end
            FarPoint.Win.Spread.CellType.NumberCellType cellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            
            //填充0
            for (int i = 3; i < neuSpread1_Sheet1.RowCount; i++)
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

            strEndCol = NumbertoString(this.neuSpread1_Sheet1.Columns.Count - 1);
            for (int introw = 3; introw < this.neuSpread1_Sheet1.Rows.Count; ++introw)
            {

                this.neuSpread1_Sheet1.Cells[introw, this.neuSpread1_Sheet1.Columns.Count - 1].Formula = string.Format("SUM({0}{2}:{1}{2})", charSum.ToString(), strEndCol, introw + 1);
                this.neuSpread1_Sheet1.Cells[introw, this.neuSpread1_Sheet1.Columns.Count - 1].CellType = cellType;
            }

        }

        private void Init()
        {
            //加载最小费用信息
            alMinFee = managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.MINFEE);
            this.neuSpread1_Sheet1.RowHeaderVisible = false;
            this.neuSpread1_Sheet1.ColumnHeaderVisible = false;
            this.neuSpread1_Sheet1.RowCount = 0;
            //this.SetReportColumnHeader(alMinFee);
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

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (neuSpread1_Sheet1.Rows.Count > 0)
            {
                int curIndex = this.neuSpread1_Sheet1.ActiveRowIndex;
                string deptCode = this.neuSpread1_Sheet1.Rows[curIndex].Tag.ToString();
                string feeCode = this.neuSpread1_Sheet1.Columns[this.neuSpread1_Sheet1.ActiveColumnIndex].Tag.ToString();
                foreach (FS.HISFC.Models.Base.Const cn in this.alMinFee)
                {
                    if (cn != null)
                    {
                        if (cn.Name.ToString() == feeCode)
                        {
                            feeCode = cn.ID.ToString();
                            break;
                        }
                    }
                }
                Common.Report rpt = new FS.SOC.Local.API.Common.Report();
                string sqlTmp = @" select (select d.dept_name from com_department d where d.dept_code = f.exec_dpcd) 科室,
                (select r.name from fin_opr_register r where r.clinic_code = f.clinic_code) 患者,
                (select e.empl_name from com_employee e where e.empl_code = f.doct_code) 医生,
                f.item_name 项目,
                (f.own_cost + f.pay_cost + f.pub_cost) 金额,
                f.fee_date 费用日期
                from fin_opb_feedetail f
                where f.fee_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                and f.fee_date <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                and f.exec_dpcd = '{2}'
                and f.fee_code = '{3}'";
                string begin = this.beginDate.Value.ToString();
                string end = this.endDate.Value.ToString();
                rpt.SqlIndex = string.Format(sqlTmp, begin, end, deptCode, feeCode);
                DataSet ds = rpt.QueryDataSet();
                this.neuSpread1_Sheet2.DataSource = ds;
                this.neuSpread1.ActiveSheetIndex = 1;
            }
        } 


    }
}
