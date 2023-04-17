using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.HealthRecord.CaseLend
{
    public partial class ucNeedBackReport : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 催还病历查询
        /// </summary>
        public ucNeedBackReport()
        {
            InitializeComponent();
        }
        FS.HISFC.BizLogic.HealthRecord.CaseCard cardMgr = new FS.HISFC.BizLogic.HealthRecord.CaseCard();
        /// <summary>
        /// 是否电子病历借阅 医生使用的申请功能 2011-8-10 by chengym
        /// </summary>
        private bool isElectronCase = false;

        /// <summary>
        /// 是否电子病历借阅属性
        /// </summary>
        [Category("是否电子病历借阅申请"), Description("处理电子病历借阅申请")]
        public bool IsElectronCase
        {
            get { return this.isElectronCase; }
            set { this.isElectronCase = value; }
        }

        /// <summary>
        /// 初始化工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.init();
            return base.OnInit(sender, neuObject, param);
        }

        private void init()
        {
            //初始化人员
            FS.HISFC.BizLogic.Manager.Person p = new FS.HISFC.BizLogic.Manager.Person();
            ArrayList al = p.GetEmployeeAll();
            this.cmbPerson.AddItems(al);
            //初始化科室
            FS.HISFC.BizLogic.Manager.Department d = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList dal = d.GetDeptmentAll();
            this.cmbDept.AddItems(dal);
        }


        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return base.OnQuery(sender, neuObject);
        }

        private void Query()
        {
            string personCode = "ALL";
            string deptCode = "ALL";
            string lendState = "1";//病历状态 1借出/2返还

            //人员
            if (this.cmbPerson.Tag != null && this.cmbPerson.Tag.ToString() != "" && this.cmbPerson.Text.Trim() != "")
            {
                personCode = this.cmbPerson.Tag.ToString();
            }
            //科室
            if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "" && this.cmbDept.Text.Trim() != "")
            {
                deptCode = this.cmbDept.Tag.ToString();
            }
            //借阅状态
            if (this.radUnBack.Checked)
            {
                lendState = "1";
            }
            else if (this.radAll.Checked)
            {
                lendState = "ALL";
            }
            else if (this.radBack.Checked)
            {
                lendState = "2";
            }
            //  借阅时间
            DateTime dtBegin = System.DateTime.MinValue;
            DateTime dtEnd = System.DateTime.MaxValue;
            if (this.checkBox1.Checked)
            {
                dtBegin = this.dateTimePicker1.Value.Date.AddHours(00).AddMinutes(00).AddSeconds(00);
                dtEnd = this.dateTimePicker2.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            }

            DataSet ds = new DataSet();
            this.cardMgr.QueryLendCaseByMoreCondition(personCode, deptCode, lendState, dtBegin, dtEnd, ref ds);
            if (ds != null)
            {
                this.fpSpread_Sheet.RowCount = 0;
                this.fpSpread_Sheet.DataSource = ds;
            }
            else
            {
                MessageBox.Show("查找不到借阅信息！", "提示");
                return;
            }

            int back = 0;
            int unback = 0;
            for (int i = 0; i < this.fpSpread_Sheet.RowCount; i++)
            {

                if (this.fpSpread_Sheet.Cells[i, 8].Text == "已还")
                {
                    back++;
                }
                else if (this.fpSpread_Sheet.Cells[i, 8].Text == "未还")
                {
                    unback++;
                }
            }
            int sum = back + unback;
            this.label1.Text = "共【" + sum + "】份，其中：未还【" + unback + "】份，已还【" + back + "】份";
        }


        public override int Print(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.PrintPage(5, 10, this.panel3);
            return base.Print(sender, neuObject);
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked == true)
            {
                this.dateTimePicker1.Enabled = true;
                this.dateTimePicker2.Enabled = true;
            }
            else if (this.checkBox1.Checked == false)
            {
                this.dateTimePicker1.Enabled = false;
                this.dateTimePicker2.Enabled = false;
            }
        }

        private void txtCaseNO_KeyDown(object sender, KeyEventArgs e)
        {
            string caseNo = "";
            caseNo = this.txtCaseNO.Text.Trim();
            //add by chengym 东莞特殊处理
            if (caseNo.IndexOf('A') >= 0 || caseNo.IndexOf('B') >= 0 || caseNo.IndexOf('C') >= 0 || caseNo.IndexOf('D') >= 0 || caseNo.IndexOf('E') >= 0)
            {
                caseNo = caseNo.Replace('A', '0');
                caseNo = caseNo.Replace('B', '0');
                caseNo = caseNo.Replace('C', '0');
                caseNo = caseNo.Replace('D', '0');
                caseNo = caseNo.Replace('E', '0');
                caseNo = caseNo.TrimStart('0').PadLeft(6, '0');
            }
            //end
            ArrayList al =this.cardMgr.QueryLendInfoByCaseNO(caseNo.PadLeft(10,'0'));
            if(al==null)
            {
                return;
            }
            this.fpSpread_Sheet.RowCount = 0;
            int back = 0;
            int unback = 0;
            foreach (FS.HISFC.Models.HealthRecord.Lend info in al)
            {
                this.fpSpread_Sheet.Rows.Add(this.fpSpread_Sheet.RowCount, 1);
                int row = this.fpSpread_Sheet.RowCount - 1;
                this.fpSpread_Sheet.Cells[row, 0].Text = info.CaseBase.PatientInfo.ID;
                this.fpSpread_Sheet.Cells[row, 1].Text = info.CaseBase.PatientInfo.Name;
                this.fpSpread_Sheet.Cells[row, 2].Text = info.CaseBase.PatientInfo.PVisit.OutTime.ToString();
                this.fpSpread_Sheet.Cells[row, 3].Text = info.EmployeeInfo.Name;
                this.fpSpread_Sheet.Cells[row, 4].Text = info.EmployeeDept.Name;
                this.fpSpread_Sheet.Cells[row, 5].Text = info.LendDate.ToString();
                FS.HISFC.BizLogic.Manager.Constant con=new FS.HISFC.BizLogic.Manager.Constant();
                FS.FrameWork.Models.NeuObject obj=con.GetConstant("CASE_LEND_TYPE",info.LendKind.ToString());
                if (obj != null && obj.ID != "")
                {
                    this.fpSpread_Sheet.Cells[row, 6].Text = obj.Name;
                }
                else
                {
                    this.fpSpread_Sheet.Cells[row, 6].Text = "";
                }
                TimeSpan d = con.GetDateTimeFromSysDateTime().Date - info.LendDate.Date;
                this.fpSpread_Sheet.Cells[row, 7].Text = d.Days.ToString();
                if (info.LendStus == "2")
                {
                    this.fpSpread_Sheet.Cells[row, 8].Text = "已还";
                    back++;
                    this.fpSpread_Sheet.Rows[row].BackColor = System.Drawing.Color.PeachPuff;
                }
                else if(info.LendStus=="1")
                {
                    unback++;
                    this.fpSpread_Sheet.Cells[row, 8].Text = "未还";
                }
                else if (info.LendStus == "3")
                {
                    this.fpSpread_Sheet.Cells[row, 8].Text = "申请，医务科未审核";
                }
                else if (info.LendStus == "4")
                {
                    this.fpSpread_Sheet.Cells[row, 8].Text = "申请，病案室未通过";
                }
            }
            int sum = back + unback;
            this.label1.Text="共【"+sum+"】份，其中：未还【"+unback+"】份，已还【"+back+"】份";
        }
    }
}