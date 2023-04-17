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

namespace FS.HISFC.Components.Speciment.OutStore
{
    public partial class ucWriteApply : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private ApplyTableManage applyTableManage;
        private SpecTypeManage specTypeManage;
        private OrgTypeManage orgTypeManage;
        private ApplyTable applyTable;
        private ApplyTable curApplyTable;
        private Employee loginPerson;
        private ArrayList arrSpecDemand; //对所申请标本的要求
        private ucSpecTypeApply ucSpecTypeApply;
        private UserApply userApply;// = new UserApply();
        private UserApplyManage userApplyManage;

        private FS.HISFC.BizProcess.Integrate.Manager dpMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService;
        string upLoadFilePath;
        string title;
        private int applyNum = 0;

        public int ApplyNum
        {
            get
            {
                return applyNum;
            }
            set
            {
                applyNum = value;
            }
        }

        public ucWriteApply()
        {
            InitializeComponent();
        }

        private void ucWriteApply_Load(object sender, EventArgs e)
        {
            ArrayList alDept = new ArrayList();
            alDept = this.dpMgr.GetDepartment();
            if ((alDept != null) && (alDept.Count > 0))
            {
                this.cmbDept.AddItems(alDept);
            }
            loginPerson = null;
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.BizLogic.Manager.DepartmentStatManager manager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            ArrayList alDepts;
            alDepts = manager.LoadAll();
            this.cmbApplyDept.AddItems(alDepts);

            ArrayList arrApplyId;
            arrApplyId = applyTableManage.QueryApplyByApplyUserID(loginPerson.ID, "U");
            this.neuCmbApplyId.AddItems(arrApplyId);

            RecentlyApply(loginPerson.ID);
        }

        private void RecentlyApply(string userApplyId)
        {
            string sql = " Select SPEC_APPLICATIONTABLE.APPLICATIONID 申请ID, SUBJECTNAME 课题名称,FUNDNAME 基金名称,FUNDSTARTTIME 基金开始时间,FUNDENDTIME 基金结束时间,\n" +
                         " Case IMPPROCESS when 'O' then '审批结束' when 'U' then '审批中' end 审批进程,\n" +
                         " case IMPRESLUT when '1' then '同意' when '0' then '拒绝' end 审批结果\n" +
                         " FROM SPEC_USERAPPLICATION RIGHT JOIN SPEC_APPLICATIONTABLE ON SPEC_USERAPPLICATION.APPLICATIONID = SPEC_APPLICATIONTABLE.APPLICATIONID\n" +
                         " WHERE SPEC_APPLICATIONTABLE.APPLICATIONID>0 and IMPPROCESS='U' and SPEC_APPLICATIONTABLE.APPLICATIONID ="+userApplyId;
            //if (rbtMonth.Checked)
            //{
            //    sql += " AND APPLYTIME >= TIMESTAMP('" + DateTime.Now.AddMonths(-1).ToString() + "')";
            //}
            //if (rbtThreeMonths.Checked)
            //{
            //    sql += " AND APPLYTIME >= TIMESTAMP('" + DateTime.Now.AddMonths(-3).ToString() + "')";
            //}
            //if (rbtAll.Checked)
            //{
            //    sql += "";
            //}
            DataSet dsTmp = new DataSet();
            applyTableManage.ExecQuery(sql, ref dsTmp);
            if (dsTmp.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = dsTmp.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["基金开始时间"] != null)
                    {
                        dt.Rows[i]["基金开始时间"] = Convert.ToDateTime(dt.Rows[i]["基金开始时间"].ToString()).ToString("yyyy-MM-dd");
                    }
                    if (dt.Rows[i]["基金结束时间"] != null)
                    {
                        dt.Rows[i]["基金结束时间"] = Convert.ToDateTime(dt.Rows[i]["基金结束时间"].ToString()).ToString("yyyy-MM-dd");
                    }
                }
                neuSpread1_Sheet1.DataSource = null;
                neuSpread1_Sheet1.DataSource = dt;
            }
            if (neuSpread1_Sheet1.RowCount == 0)
            {
                neuSpread1_Sheet1.Visible = false;
            }
        }

        /// <summary>
        /// 根据申请表ID初始化申请表
        /// </summary>
        private void InitApplyTable()
        {
            //lsSpecOut.View = View.List;
            //DiabledSomeControls(); 
            curApplyTable = new ApplyTable();
            curApplyTable = applyTableManage.QueryApplyByID(ApplyNum.ToString());
            textApplyName.Text = curApplyTable.ApplyUserName;
            cmbDept.Text = curApplyTable.DeptName;
        }
    }
}
