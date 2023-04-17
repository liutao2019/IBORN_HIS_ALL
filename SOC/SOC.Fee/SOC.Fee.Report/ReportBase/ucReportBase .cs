using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Fee.Report.Base
{
    /// <summary>
    /// 常规报表基类
    /// </summary>
    public partial class ucReportBase : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucReportBase()
        {
            InitializeComponent();

            lstSpread = new List<FS.FrameWork.WinForms.Controls.NeuSpread>();
            lstSpread.Add(neuSpread1);
            lstSpread.Add(neuSpread2);
            lstSpread.Add(neuSpread3);

            lstTitleText = new List<Label>();
            lstTitleText.Add(lblTitle1);
            lstTitleText.Add(lblTitle2);
            lstTitleText.Add(lblTitle3);
        }

        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
        FS.SOC.HISFC.BizProcess.Fee.Report.InpatientFee inpatientReport = new FS.SOC.HISFC.BizProcess.Fee.Report.InpatientFee();

        DateTime dtBegin = DateTime.MinValue;
        DateTime dtEnd = DateTime.MinValue;

        List<FS.FrameWork.WinForms.Controls.NeuSpread> lstSpread = null;
        List<Label> lstTitleText = null;

        #region 属性
        /// <summary>
        /// 报表名称
        /// </summary>
        List<string> lstReportTitle = new List<string>();
        /// <summary>
        /// 报表名称，最多支持三张报表
        /// </summary>
        [Category("报表设置"), Description("报表名称，多个报表时，以‘|’隔开，最多三个")]
        public string ReportTitle
        {
            get
            {
                if (lstReportTitle == null)
                {
                    return string.Empty;
                }
                else
                {
                    string strTitle = "";
                    foreach (string str in lstReportTitle)
                    {
                        strTitle += str + "|";
                    }
                    return strTitle;
                }
            }
            set 
            {
                if (lstReportTitle == null)
                {
                    lstReportTitle = new List<string>();
                }
                else
                {
                    lstReportTitle.Clear();
                }

                if (!string.IsNullOrEmpty(value))
                {
                    string[] strArr = value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    lstReportTitle.AddRange(strArr);
                }
            }
        }

        List<string> lstReportSQL = new List<string>();
        /// <summary>
        /// 报表SQL语句ID
        /// </summary>
        [Category("报表设置"), Description("报表SQL语句ID，多个报表时，每行一个，最多三个")]
        public string ReportSQL
        {
            get 
            {
                if (lstReportSQL == null)
                {
                    return string.Empty;
                }
                else
                {
                    string strSQL = "";
                    foreach (string str in lstReportSQL)
                    {
                        strSQL += str + "|";
                    }
                    return strSQL;
                }
            }
            set
            {
                if (lstReportSQL == null)
                {
                    lstReportSQL = new List<string>();
                }
                else
                {
                    lstReportSQL.Clear();
                }
                if (!string.IsNullOrEmpty(value))
                {
                    string[] strArr = value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    lstReportSQL.AddRange(strArr);
                }
            }
        }
        /// <summary>
        /// 是否包含时间条件
        /// </summary>
        [Category("报表设置"), Description("报表是否包含时间条件")]
        public bool BlnTimeCondition
        {
            get { return pnlTime.Visible; }
            set { pnlTime.Visible = value; }
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
        /// 报表是否包含合同单位条件
        /// </summary>
        [Category("报表设置"), Description("报表是否包含合同单位条件")]
        public bool BlnPactCondition
        {
            get { return pnlPact.Visible; }
            set { pnlPact.Visible = value; }
        }

        private bool blnAutoAddSumary = false;
        /// <summary>
        /// 是否自动增加汇总信息
        /// </summary>
        [Category("报表设置"), Description("是否自动增加汇总信息")]
        public bool BlnAutoAddSumary
        {
            get { return blnAutoAddSumary; }
            set { blnAutoAddSumary = value; }
        }
        /// <summary>
        /// 不需要自动汇总的列
        /// </summary>
        [Category("报表设置"), Description("不需要自动汇总的列")]
        public string NoAutoSumaryColumn
        {
            get { return strNoAutoSumaryColumn; }
            set { strNoAutoSumaryColumn = value; }
        }
        string strNoAutoSumaryColumn = string.Empty;

        private bool blnAutoRowSumary = false;
        /// <summary>
        /// 是否自动增加行汇总信息
        /// </summary>
        [Category("报表设置"), Description("是否自动增加行汇总信息")]
        public bool BlnAutoRowSumary
        {
            get { return blnAutoRowSumary; }
            set { blnAutoRowSumary = value; }
        }

        string strNoAutoRowSumaryColumn = string.Empty;
        /// <summary>
        /// 不参加行汇总的列
        /// </summary>
        [Category("报表设置"), Description("不参加行汇总的列")]
        public string NoAutoRowSumaryColumn
        {
            get { return strNoAutoRowSumaryColumn; }
            set { strNoAutoRowSumaryColumn = value; }
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
        /// <summary>
        /// 交叉报表，关联列名称，多个以‘,’分开；各报表之间用‘|’隔开
        /// </summary>
        [Category("报表设置"), Description("交叉报表，关联列名称，多个以‘,’分开；各报表之间用‘|’隔开")]
        public string CrossReportCol
        {
            get
            {
                if (lstCrossReportCol == null || lstCrossReportCol.Count <= 0)
                {
                    return string.Empty;
                }

                string strRes = string.Empty;
                foreach (List<string> lstCross in lstCrossReportCol)
                {
                    foreach (string str in lstCross)
                    {
                        strRes += str + ",";
                    }
                    strRes = strRes.Trim(new char[] { ',' });

                    strRes += "|";
                }

                strRes = strRes.Trim(new char[] { '|' });
                return strRes;
            }
            set
            {
                if (lstCrossReportCol == null)
                {
                    lstCrossReportCol = new List<List<string>>();
                }
                else
                {
                    lstCrossReportCol.Clear();
                }

                if (!string.IsNullOrEmpty(value))
                {
                    List<string> lstCorss = null;
                    string[] strCorssArr = value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strCorss in strCorssArr)
                    {
                        lstCorss = null;
                        string[] strArr = strCorss.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string str in strArr)
                        {
                            if (lstCorss == null)
                            {
                                lstCorss = new List<string>();
                            }
                            lstCorss.Add(str);
                        }

                        lstCrossReportCol.Add(lstCorss);
                    }
                }
            }
        }
        /// <summary>
        /// 交叉报表，关联列名称
        /// </summary>
        List<List<string>> lstCrossReportCol = new List<List<string>>();
        /// <summary>
        /// 从表统计列名称。各报表之间用‘|’隔开
        /// </summary>
        [Category("报表设置"), Description("从表统计Group列名称。各报表之间用‘|’隔开")]
        public string SecondTabColName
        {
            get
            {
                string strRes = string.Empty;
                if (lstTabColName == null || lstTabColName.Count <= 0)
                    return strRes;

                foreach (string str in lstTabColName)
                {
                    strRes += str + "|";
                }

                strRes = strRes.Trim(new char[] { '|' });
                return strRes;
            }
            set
            {
                if(lstTabColName == null)
                {
                    lstTabColName = new List<string>();
                }
                else
                {
                    lstTabColName.Clear();
                }
                if (!string.IsNullOrEmpty(value))
                {
                    lstTabColName.AddRange(value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries));
                }
            }
        }
        /// <summary>
        /// 从表统计Group列名称
        /// </summary>
        List<string> lstTabColName = null;
        /// <summary>
        /// 从表统计列名称
        /// </summary>
        [Category("报表设置"), Description("从表统计列名称。各报表之间用‘|’隔开")]
        public string StatColName
        {
            get
            {
                string strRes = string.Empty;
                if (lstStatColName == null || lstStatColName.Count <= 0)
                    return strRes;

                foreach (string str in lstStatColName)
                {
                    strRes += str + "|";
                }

                strRes = strRes.Trim(new char[] { '|' });
                return strRes;
            }
            set
            {
                if (lstStatColName == null)
                {
                    lstStatColName = new List<string>();
                }
                else
                {
                    lstStatColName.Clear();
                }
                if (!string.IsNullOrEmpty(value))
                {
                    lstStatColName.AddRange(value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries));
                }
            }
        }
        /// <summary>
        /// 从表统计列名称
        /// </summary>
        List<string> lstStatColName = null;
        #endregion


        private void InitBase()
        {
            if (this.DesignMode)
                return;

            dtEnd = conMgr.GetDateTimeFromSysDateTime();

            dtBegin = dtEnd.AddDays(-1);
            this.neuDateTimePicker1.Value = dtBegin;
            this.neuDateTimePicker2.Value = dtEnd;

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
            ArrayList arlEmployee = this.managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
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
            this.cmbEmployee.AddItems(arlEmployeeList);
            this.cmbEmployee.SelectedIndex = 0;

            // 合同单位
            ArrayList arlPact = this.managerIntegrate.QueryPactUnitAll();
            this.cmbPact.AddItems(arlPact);
            this.cmbPact.SelectedIndex = 0;

            if (lstReportTitle == null || lstReportTitle.Count <= 0)
            {
                MessageBox.Show("报表名称为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (lstReportSQL == null || lstReportSQL.Count <= 0)
            {
                MessageBox.Show("报表SQL ID 为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            int iTabPageIdx = tabReport.TabPages.Count;
            if (lstReportSQL.Count < iTabPageIdx)
            {
                iTabPageIdx = lstReportSQL.Count;
            }
            if (lstReportTitle.Count < iTabPageIdx)
            {
                iTabPageIdx = lstReportTitle.Count;
            }
            for (int idx = tabReport.TabPages.Count; idx > 0; idx--)
            {
                if (idx > iTabPageIdx)
                {
                    tabReport.TabPages.RemoveAt(idx - 1);
                }
            }

            for (int idx = 0; idx < tabReport.TabPages.Count; idx++)
            {
                tabReport.TabPages[idx].Text = lstReportTitle[idx];
                lstTitleText[idx].Text = lstReportTitle[idx];
            }

        }
        protected virtual void Init()
        {

        }

        /// <summary>
        /// 查询
        /// </summary>
        protected virtual DataTable Query(string strSQL, object[] param)
        {
            DataTable dtResult = null;

            if (string.IsNullOrEmpty(strSQL))
            {
                return dtResult;
            }

            int iRes = inpatientReport.QuerySQL(strSQL, out dtResult, param);
            if (iRes < 0)
            {
                dtResult = null;
                MessageBox.Show(inpatientReport.Err, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return dtResult;
        }
        /// <summary>
        /// 交叉报表查询
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="lstCrossCol">交叉报表，关联列名称</param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected virtual DataTable Query(string strSQL, List<string> lstCrossCol, string tabColName, string statColName, object[] param)
        {
            DataTable dtResult = null;

            if (string.IsNullOrEmpty(strSQL))
            {
                return dtResult;
            }

            string[] sqlArr = strSQL.Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);

            List<DataTable> lstResult = new List<DataTable>();
            DataTable dtTemp = null;
            foreach (string sql in sqlArr)
            {
                int iRes = inpatientReport.QuerySQL(sql, out dtTemp, param);
                if (iRes < 0)
                {
                    dtTemp = null;
                    MessageBox.Show(inpatientReport.Err, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }

                lstResult.Add(dtTemp);
            }

            dtResult = CorssTable(lstResult, lstCrossCol, tabColName, statColName);


            return dtResult;
        }

        /// <summary>
        /// 组织交叉报表
        /// </summary>
        /// <param name="lstTable"></param>
        /// <param name="lstCrossCol"></param>
        /// <returns></returns>
        private DataTable CorssTable(List<DataTable> lstTable, List<string> lstCrossCol, string TabColName, string statColName)
        {
            if (lstTable == null || lstTable.Count <= 0)
            {
                return null;
            }
            if (lstCrossCol == null || lstCrossCol.Count <= 0)
            {
                return lstTable[0];
            }

            if (string.IsNullOrEmpty(TabColName))
            {
                return lstTable[0];
            }

            // 第一张表作为主表
            DataTable dtResult = lstTable[0];
            DataTable dtColumn = null;
            DataTable dtTemp = null;
            DataColumn dcTemp = null;
            decimal decTemp = 0;
            string strFilter = "";

            for (int idx = 1; idx < lstTable.Count; idx++)
            {
                dtTemp = lstTable[idx];
                if (dtTemp == null || dtTemp.Rows.Count <= 0)
                {
                    continue;
                }
                dtColumn = dtTemp.DefaultView.ToTable(true, new string[] { TabColName });

                foreach (DataRow drColumn in dtColumn.Rows)
                {
                    string ColName = drColumn[TabColName].ToString();
                    dcTemp = new DataColumn(ColName, typeof(decimal));
                    dcTemp.DefaultValue = 0;
                    
                    dtResult.Columns.Add(dcTemp);

                    decTemp = 0;
                    strFilter = "";

                    foreach (DataRow drRes in dtResult.Rows)
                    {
                        strFilter = GetFilter(drRes, lstCrossCol, TabColName, ColName);
                        decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtTemp.Compute("Sum(" + statColName + ")", strFilter));

                        drRes[ColName] = decTemp;
                    }
                }
            }

            return dtResult;

        }
        /// <summary>
        /// 获取统计条件
        /// </summary>
        /// <param name="drMain"></param>
        /// <param name="lstCrossCol"></param>
        /// <param name="staColName"></param>
        /// <returns></returns>
        private string GetFilter(DataRow drMain, List<string> lstCrossCol, string TabColName, string staColName)
        {
            string strFilter = string.Empty;
            if (lstCrossCol == null || lstCrossCol.Count <= 0)
            {
                strFilter = staColName + " = '" + staColName + "'";
            }
            else
            {
                string strTemp = "";
                for (int idx = 0; idx < lstCrossCol.Count; idx++)
                {
                    strTemp = lstCrossCol[idx];
                    strFilter += strTemp + " = '" + drMain[strTemp] + "' and ";
                }
                strFilter += TabColName + " = '" + staColName + "'";
            }

            return strFilter;
        }


        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear()
        {
            int idx = this.tabReport.SelectedIndex;
            if (lstSpread.Count <= idx)
            {
                if (this.lstSpread[idx].ActiveSheet.RowCount > 0)
                {
                    lstSpread[idx].ActiveSheet.RowCount = 0;
                }
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void Print()
        {
            if (this.tabReport.TabPages.Count <= 0)
                return;

            int idx = this.tabReport.SelectedIndex;

            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.PrintPage(0, 0, tabReport.TabPages[idx]);

        }

        /// <summary>
        /// 导出
        /// </summary>
        private void Export()
        {
            if (this.tabReport.TabPages.Count <= 0)
                return;

            int idx = this.tabReport.SelectedIndex;
            if (idx < lstSpread.Count)
            {
                if (lstSpread[idx].Export() == 1)
                {
                    MessageBox.Show("导出成功");

                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            this.InitBase();
            this.Init();
            base.OnLoad(e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.tabReport.TabPages.Count <= 0)
                return 1;

            int idx = this.tabReport.SelectedIndex;
            string strSqlID = this.lstReportSQL[idx];
            object[] param = GetParams();
            List<string> lstCrossCol = null;
            if (lstCrossReportCol != null && lstCrossReportCol.Count > idx)
            {
                lstCrossCol = lstCrossReportCol[idx];
            }

            string strTabColName = string.Empty;
            if (lstTabColName != null && lstTabColName.Count > idx)
            {
                strTabColName = lstTabColName[idx];
            }
            string strStatColName = string.Empty;
            if (lstStatColName != null && lstStatColName.Count > idx)
            {
                strStatColName = lstStatColName[idx];
            }

            DataTable dtResult = null;
            if ((lstCrossCol == null || lstCrossCol.Count <= 0) || string.IsNullOrEmpty(strTabColName) || string.IsNullOrEmpty(strStatColName))
            {
                dtResult = this.Query(strSqlID, param);
            }
            else
            {
                dtResult = this.Query(strSqlID, lstCrossCol, strTabColName, strStatColName, param);
            }

            if (dtResult == null)
                return 1;

            this.DoWithShow(ref dtResult);

            lstSpread[idx].ActiveSheet.DataSource = dtResult;

            return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();
            return 1;
        }
        public override int Export(object sender, object neuObject)
        {
            this.Export();
            return base.Export(sender, neuObject);
        }
        #region 获取参数
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <returns></returns>
        protected virtual object[] GetParams()
        {
            List<object> lstObject = new List<object>();
            if (pnlTime.Visible)
            {
                lstObject.Add(this.neuDateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00"));
                lstObject.Add(this.neuDateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59"));
            }

            if (pnlDept.Visible)
            {
                lstObject.Add(this.cmbDept.SelectedItem.ID);
            }

            if (pnlEmployee.Visible)
            {
                lstObject.Add(this.cmbEmployee.SelectedItem.ID);
            }

            if (pnlPact.Visible)
            {
                lstObject.Add(this.cmbPact.SelectedItem.ID);
            }

            return lstObject.ToArray();
        }
        /// <summary>
        /// 获取开始时间
        /// </summary>
        /// <returns></returns>
        protected virtual string GetStartTimeParms()
        {
            string strStartTime = "";
            if (pnlTime.Visible)
            {
                strStartTime = this.neuDateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00");
            }
            return strStartTime;
        }

        protected virtual string GetEndTimeParms()
        {
            string strEndTime = "";
            if (pnlTime.Visible)
            {
                strEndTime = this.neuDateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59");
            }
            return strEndTime;
        }

        /// <summary>
        /// 获取科室
        /// </summary>
        /// <returns></returns>
        protected virtual string GetDeptParms()
        {
            string strDept = "";
            if (pnlDept.Visible && this.cmbDept.SelectedItem != null)
            {
                strDept = this.cmbDept.SelectedItem.ID;
            }
            return strDept;
        }
        /// <summary>
        /// 获取医生
        /// </summary>
        /// <returns></returns>
        protected virtual string GetEmployeeParms()
        {
            string strEmp = "";
            if (pnlEmployee.Visible && this.cmbEmployee.SelectedItem != null)
            {
                strEmp = cmbEmployee.SelectedItem.ID;
            }
            return strEmp;
        }
        /// <summary>
        /// 获取合同单位
        /// </summary>
        /// <returns></returns>
        protected virtual string GetPactParms()
        {
            string strPact = "";
            if (this.pnlPact.Visible && cmbPact.SelectedItem != null)
            {
                strPact = cmbPact.SelectedItem.ID;
            }
            return strPact;
        }

        #endregion
        /// <summary>
        /// 显示结果前特殊处理
        /// </summary>
        /// <param name="dtResult"></param>
        protected virtual void DoWithShow(ref DataTable dtResult)
        {
            if (dtResult == null || dtResult.Rows.Count <= 0)
            {
                return;
            }

            if (blnAutoAddSumary)
            {
                try
                {
                    DataRow drNew = dtResult.NewRow();
                    drNew[0] = "合计：";

                    foreach (DataColumn dc in dtResult.Columns)
                    {
                        try
                        {
                            if (strNoAutoSumaryColumn.Contains(dc.ColumnName))
                                continue;
                            //if(dc.DataType == typeof(decimal) || dc.DataType == typeof(int) || dc.DataType == )
                            drNew[dc] = FS.FrameWork.Function.NConvert.ToDecimal(dtResult.Compute("Sum(" + dc.ColumnName + ")", ""));
                        }
                        catch
                        {
                        }
                    }
                    dtResult.Rows.Add(drNew);
                }
                catch
                { }
            }

            if (blnAutoRowSumary)
            {
                DataColumn dc = new DataColumn("合计", typeof(decimal));
                dc.DefaultValue = 0;

                dtResult.Columns.Add(dc);

                decimal decTemp = 0;
                decimal decSumary = 0;
                foreach (DataRow dr in dtResult.Rows)
                {
                    decSumary = 0;
                    decTemp = 0;
                    foreach (DataColumn dcTemp in dtResult.Columns)
                    {
                        if (strNoAutoRowSumaryColumn.Contains(dcTemp.ColumnName))
                            continue;

                        decimal.TryParse(dr[dcTemp.ColumnName].ToString(), out decTemp);

                        decSumary += decTemp;
                    }

                    dr[dc.ColumnName] = decSumary;
                }
            }
        }
        /// <summary>
        /// 设置合同单位选项
        /// </summary>
        /// <param name="arlPact"></param>
        public void SetPactUnit(ArrayList arlPact)
        {
            if (arlPact == null || arlPact.Count <= 0)
                return;
            this.cmbPact.alItems.Clear();
            this.cmbPact.AddItems(arlPact);
            this.cmbPact.SelectedIndex = 0;
        }

    }
}
