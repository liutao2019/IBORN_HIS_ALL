using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using FS.HISFC.Models.Base;
using System.Collections;

namespace FS.HISFC.Components.Speciment.Report
{
    public partial class ucSendDetSta : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        private bool lDept = true;
        private bool lDoc = true;
        private string permission = "";
        private string deptSql = "";
        private string docSql = "";
        private FS.HISFC.Models.Base.Employee loginPerson = new FS.HISFC.Models.Base.Employee();
        private string[] condition = new string[5];
        private string curSql = "";
        private string curSql1 = "";
        private string curSql2 = "";
        private string curSql3 = "";
        private string curSql4 = "";
        
        private string sql = "";
        private string order = "";
        private string title = "";
        private string title1 = "";
        private string title2 = "";
        private string title3 = "";
        private string title4 = "";
        private string deptStatSql = "";
        private string docStatSql = "";

        private DataTable dtSource = new DataTable();

        private SpecSourceManage dbMgr = new SpecSourceManage();

        /// <summary>
        /// 设置ldept的Visible属性
        /// </summary>
        public bool Ldept
        {
            set
            {
                lDept = value;
            }
        }

        /// <summary>
        /// 设置lblDoc的Visible属性
        /// </summary>
        public bool Ldoc
        {
            set
            {
                lDoc = value;
            }
        }

        /// <summary>
        /// 当前的数据源
        /// </summary>
        public DataTable CurDataSource
        {
            set
            {
                dtSource = value;
            }
        }

        /// <summary>
        /// 权限 P,个人， D，部门 , A 全部
        /// </summary>
        public string Permission
        {
            set
            {
                permission = value;
            }
            get
            {
                return permission;
            }
        }

        /// <summary>
        /// Dept字段的条件
        /// </summary>
        public string SqlDept
        {
            set
            {
                deptSql = value;
            }
            get
            {
                return deptSql;
            }
        }

        /// <summary>
        /// 送存医生字段的条件
        /// </summary>
        public string SqlDoc
        {
            set
            {
                docSql = value;
            }
            get
            {
                return docSql;
            }
        }

        public string Sql
        {
            set
            {
                sql = "";
            }
            get
            {
                return sql;
            }
        }

        /// <summary>
        /// 报表的表头
        /// </summary>
        public string Title
        {
            set
            {
                title = value;
            }
            get
            {
                return title;
            }
        }

        /// <summary>
        /// Sheet2的表头
        /// </summary>
        public string Title1
        {
            get
            {
                return title1;
            }
            set
            {
                title1 = value;
            }
        }

        /// <summary>
        /// Sheet3的表头
        /// </summary>
        public string Title2
        {
            get
            {
                return title2;
            }
            set
            {
                title2 = value;
            }
        }

        public string Title3
        {
            get
            {
                return title3;
            }
            set
            {
                title3 = value;
            }
        }

        public string Title4
        {
            get
            {
                return title4;
            }
            set
            {
                title4 = value;
            }
        }
        /// <summary>
        /// 排序的字段
        /// </summary>
        public string Order
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
            }
        }

        private void GenerSql()
        {
            string tmp = @"select 标本号,病种,sum(分装数量) 分装数量,科室,送存医生,送存时间,性别,标本类型 from (";

            curSql = "\n select distinct \n" +
                     " ss.spec_no 标本号,\n" +
                     " sd.DISEASENAME 病种, (select count(*) from SPEC_SUBSPEC sp where sp.SPECID = ss.SPECID) 分装数量,\n" +
                     " ( select d.DEPT_NAME  from com_deptstat d where d.DEPT_CODE = ss.DEPTNO fetch first rows only) 科室, \n" +
                     " ( select e.EMPL_NAME from COM_EMPLOYEE e where e.EMPL_CODE = ss.SENDDOCID) 送存医生, \n" +
                     " substr(varchar ( ss.SENDDATE),1,10) 送存时间, \n" +
                     " (case LENGTH(ss.INPATIENT_NO) when 14 then substr(ss.INPATIENT_NO,5,10) when 0 then p.CARD_NO else ss.INPATIENT_NO end) 病历号, p.NAME 病人姓名, (case p.GENDER when 'M' then '男' else '女' end) 性别,\n" +
                     " (case ss.ORGORBLOOD when 'B' then '血' else '组织' end) 标本类型 \n" +
                     " from SPEC_SOURCE ss left join SPEC_SOURCE_STORE st on ss.SPECID = st.SPECID \n" +
                     " left join SPEC_SUBSPEC s on ss.SPECID = s.SPECID \n" +
                     " left join SPEC_DISEASETYPE sd on sd.DISEASETYPEID = ss.DISEASETYPEID left join SPEC_PATIENT p on p.PATIENTID = ss.PATIENTID \n" +
                     " where ss.specid in ( select max(specid) from spec_source s1"+
                     " where s1.SENDDATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss') and s1.SENDDATE<=to_date('{1}','yyyy-mm-dd hh24:mi:ss') and s1.ORGORBLOOD = '{4}' group by s1.spec_no, s1.DISEASETYPEID)";
            curSql = tmp + curSql;
            if (rbtBld.Checked)
            {
                curSql1 = dbMgr.ExecSqlReturnOne("select db2_sql from com_sql where id = 'SendSpecStat.Dept'");
                curSql2 = dbMgr.ExecSqlReturnOne("select db2_sql from com_sql where id = 'SendSpecStat.Doc'");
                curSql3 = dbMgr.ExecSqlReturnOne("select db2_sql from com_sql where id = 'SendSpecStat.ParDept'");
                curSql4 = dbMgr.ExecSqlReturnOne("select db2_sql from com_sql where id = 'SendSpecStat.DeptAndPar'");
            }
            else
            {
                curSql1 = dbMgr.ExecSqlReturnOne("select db2_sql from com_sql where id = 'SendSpecStat.DeptOrg'");
                curSql2 = dbMgr.ExecSqlReturnOne("select db2_sql from com_sql where id = 'SendSpecStat.DocOrg'");
                curSql3 = dbMgr.ExecSqlReturnOne("select db2_sql from com_sql where id = 'SendSpecStat.ParDeptOrg'");
                curSql4 = dbMgr.ExecSqlReturnOne("select db2_sql from com_sql where id = 'SendSpecStat.DeptAndParOrg'");
            }
            
        }

        private void SetVisible()
        {
            lblDept.Visible = lDept;
            cmbDept.Visible = lDept;

            lblDoc.Visible = lDoc;
            cmbDoc.Visible = lDoc;
        }

        private void SetTitle()
        {
            neuSpread1_Sheet1.Models.ColumnHeaderSpan.Add(0, 0, 1, neuSpread1_Sheet1.Columns.Count);
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].Text = title;
            this.neuSpread1_Sheet2.SheetName = title;

            neuSpread1_Sheet2.Models.ColumnHeaderSpan.Add(0, 0, 1, neuSpread1_Sheet2.Columns.Count);
            this.neuSpread1_Sheet2.ColumnHeader.Cells[0, 0].Text = title1;
            neuSpread1_Sheet2.SheetName = title1;

            neuSpread1_Sheet3.Models.ColumnHeaderSpan.Add(0, 0, 1, neuSpread1_Sheet3.Columns.Count);
            this.neuSpread1_Sheet3.ColumnHeader.Cells[0, 0].Text = title2;
            neuSpread1_Sheet3.SheetName = title2;

            neuSpread1_sheetView2.Models.ColumnHeaderSpan.Add(0, 0, 1, neuSpread1_sheetView2.Columns.Count);
            this.neuSpread1_sheetView2.ColumnHeader.Cells[0, 0].Text = title3;
            neuSpread1_sheetView2.SheetName = title3;

            neuSpread1_Sheet4.Models.ColumnHeaderSpan.Add(0, 0, 1, neuSpread1_Sheet4.Columns.Count);
            this.neuSpread1_Sheet4.ColumnHeader.Cells[0, 0].Text = title4;
            neuSpread1_Sheet4.SheetName = title4;
        }

        private void DeptBinding()
        {
            ArrayList alDepts = new ArrayList();
            FS.HISFC.BizLogic.Manager.DepartmentStatManager manager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            alDepts = manager.GetMultiDept(loginPerson.ID);
            this.cmbDept.AddItems(alDepts);
        }

        private void DataSourceBinding()
        {
            this.neuSpread1_Sheet1.RowCount = 0;
            this.dtSource = new DataTable();
            DateTime dtOrgMin = new DateTime(2010, 8, 5);
            condition[0] = dtpStartDate.Value.Date.ToString();
            condition[1] = dtpEndTime.Value.Date.AddDays(1.0).ToString();
            if (rbtOrg.Checked && dtpStartDate.Value < dtOrgMin)
            {
                condition[0] = dtOrgMin.ToString();
            }
            GenerSql();

            if (permission == "D")
            {
                condition[2] = loginPerson.Dept.ID;
                if (cmbDoc.Tag.ToString() == "" || cmbDoc.Text == "")
                {

                }
                else
                {
                    condition[3] = cmbDoc.Tag.ToString();
                    curSql += " " + docSql;
                    curSql2 += " " + docSql;
                }
            }
            if (permission == "A")
            {
                if (cmbDept.Tag.ToString() == "" || cmbDept.Text == "")
                {

                }
                else
                {
                    condition[2] = cmbDept.Tag.ToString();// == "" ? loginPerson.Dept.ID : cmbDept.Tag.ToString();
                    curSql += " " + deptSql;
                    curSql1 += " " + deptSql;
                }

                if (cmbDoc.Tag.ToString() == "" || cmbDoc.Text == "")
                {

                }
                else
                {
                    condition[3] = cmbDoc.Tag.ToString();
                    curSql += " " + docSql;
                    curSql2 += " " + docSql;
                }
            }
            else
            {
                condition[2] = loginPerson.Dept.ID;
                condition[3] = loginPerson.ID;
            }

            if (rbtBld.Checked)
            {
                condition[4] = "B";
            }
            else
            {
                condition[4] = "O";
            }
            string querSql = "";
            string querSql1 = "";
            string querSql2 = "";
            string querySql3 = "";
            string querySql4 = "";
            querSql = string.Format(curSql, condition);
            querSql1 = string.Format(curSql1, condition);
            querSql2 = string.Format(curSql2, condition);
            querSql += order;
            querySql3 = string.Format(curSql3, condition);
            querySql4 = string.Format(curSql4, condition);
            //querSql += " order by 科室 , 送存医生";
            //if (rbtDept.Checked)
            //{
            //    querSql = string.Format(curSql, condition);
            //}
            //if (rbtDoc.Checked)
            //{
            //    querSql = string.Format(docSql, condition);
            //}
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            DataSet ds2 = new DataSet();
            DataSet ds3 = new DataSet();
            DataSet ds4 = new DataSet();

            querSql += @" ) a group by  标本号,病种,科室,送存医生,送存时间,性别,标本类型";
            dbMgr.ExecQuery(querSql,ref ds);
            dbMgr.ExecQuery(querSql1, ref ds1);
            dbMgr.ExecQuery(querSql2, ref ds2);
            dbMgr.ExecQuery(querySql3, ref ds3);
            dbMgr.ExecQuery(querySql4, ref ds4);

            if (ds == null || ds.Tables.Count <= 0)
            {
               
            }
            else
            {
                dtSource = ds.Tables[0];
                neuSpread1_Sheet1.DataSource = dtSource;
                for (int i = 0; i < neuSpread1_Sheet1.Columns.Count; i++)
                {
                    neuSpread1_Sheet1.Columns[i].Width = 1100 / dtSource.Columns.Count;
                    neuSpread1_Sheet1.Columns[i].AllowAutoFilter = true;
                    neuSpread1_Sheet1.Columns[i].AllowAutoSort = true;
                }
            }
            if (ds1 == null || ds1.Tables.Count <= 0)
            {
                
            }
            else
            {
                dtSource = new DataTable();
                dtSource = ds1.Tables[0];
                neuSpread1_Sheet2.DataSource = dtSource;
                //this.SetTitle();
                for (int i = 0; i < neuSpread1_Sheet2.Columns.Count; i++)
                {
                    neuSpread1_Sheet2.Columns[i].Width = 150;
                    neuSpread1_Sheet2.Columns[i].AllowAutoFilter = true;
                    neuSpread1_Sheet2.Columns[i].AllowAutoSort = true;
                }
            }
            if (ds2 == null || ds2.Tables.Count <= 0)
            {
                
            }
            else
            {
                dtSource = new DataTable();
                dtSource = ds2.Tables[0];
                neuSpread1_Sheet3.DataSource = dtSource;
                for (int i = 0; i < neuSpread1_Sheet3.Columns.Count; i++)
                {
                    neuSpread1_Sheet3.Columns[i].Width = 150;
                    neuSpread1_Sheet3.Columns[i].AllowAutoFilter = true;
                    neuSpread1_Sheet3.Columns[i].AllowAutoSort = true;
                }
            }
            if (ds3 == null || ds3.Tables.Count <= 0)
            {

            }
            else
            {
                dtSource = new DataTable();
                dtSource = ds3.Tables[0];
                neuSpread1_sheetView2.DataSource = dtSource;
                for (int i = 0; i < neuSpread1_sheetView2.Columns.Count; i++)
                {
                    neuSpread1_sheetView2.Columns[i].Width = 150;
                    neuSpread1_sheetView2.Columns[i].AllowAutoFilter = true;
                    neuSpread1_sheetView2.Columns[i].AllowAutoSort = true;
                }
            }
            if (ds4 == null || ds4.Tables.Count <= 0)
            {

            }
            else
            {
                dtSource = new DataTable();
                dtSource = ds4.Tables[0];
                neuSpread1_Sheet4.DataSource = dtSource;
                //this.SetTitle();
                for (int i = 0; i < neuSpread1_Sheet4.Columns.Count; i++)
                {
                    neuSpread1_Sheet4.Columns[i].Width = 150;
                    neuSpread1_Sheet4.Columns[i].AllowAutoFilter = true;
                    neuSpread1_Sheet4.Columns[i].AllowAutoSort = true;
                }
            }
            this.SetTitle();
        }

        public ucSendDetSta()
        {
            InitializeComponent();
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
        }

        protected override void OnLoad(EventArgs e)
        {
            SetVisible();
            dtpStartDate.Value = DateTime.Now.AddMonths(-1);
            SetTitle();
            FS.HISFC.BizLogic.Manager.DepartmentStatManager deptMgr = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            ArrayList alDepts;
            neuSpread1_sheetView2.Visible = false;
            if (permission == "D")
            {
                cmbDept.ClearItems();
                alDepts = new ArrayList();
                deptMgr.GetMultiDept(loginPerson.ID);
                cmbDept.AddItems(alDepts);
                //rbtDept.Checked = true;
            }
            if (permission == "A")
            {
                cmbDept.ClearItems();
                alDepts = new ArrayList();
                alDepts = deptMgr.LoadAll();
                cmbDept.AddItems(alDepts);
                neuSpread1_sheetView2.Visible = true;
                //rbtDept.Checked = true;
            }
            else
            {
                cmbDept.ClearItems();
                //rbtDoc.Checked = true;
                //rbtDoc.Visible = false;
                //rbtDept.Visible = false;
            }
            cmbDept.Text = "";
            cmbDoc.Text = "";
            base.OnLoad(e);
        }

        public override int Query(object sender, object neuObject)
        {
            this.DataSourceBinding();
            return base.Query(sender, neuObject);
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDept.Tag.ToString() == "" || cmbDept.Text == "")
            {
                return;
            }
            FS.HISFC.BizLogic.Manager.Person personList = new FS.HISFC.BizLogic.Manager.Person();
            ArrayList arrPerson = personList.GetEmployee(EnumEmployeeType.D, cmbDept.Tag.ToString());
            cmbDoc.AddItems(arrPerson);
            cmbDoc.Text = "";
        }

        public override int Export(object sender, object neuObject)
        {
            string path = string.Empty;
            SaveFileDialog saveFileDiaglog = new SaveFileDialog();

            saveFileDiaglog.Title = "查询结果导出，选择Excel文件保存位置";
            saveFileDiaglog.RestoreDirectory = true;
            saveFileDiaglog.InitialDirectory = Application.StartupPath;
            saveFileDiaglog.Filter = "Excel files (*.xls)|*.xls";
            if (cmbDoc.Text == "")
            {
                if (cmbDept.Text == "")
                {
                    saveFileDiaglog.FileName = DateTime.Now.ToShortDateString() + " 标本送存量统计";
                }
                else
                {
                    saveFileDiaglog.FileName = cmbDept.Text + " 标本送存量统计";
                }
            }
            else
            {
                saveFileDiaglog.FileName =  cmbDoc.Text + " 标本送存量统计";
            }           
            DialogResult dr = saveFileDiaglog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                this.neuSpread1.SaveExcel(saveFileDiaglog.FileName, FarPoint.Excel.ExcelSaveFlags.SaveCustomColumnHeaders);
            }
            return base.Export(sender, neuObject);
        }
    }
}
