using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.CaseHistory
{
    public partial class ucNotCallBack : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucNotCallBack()
        {
            InitializeComponent();
        }

        DataSet ds = new DataSet();

        private CallbackStatus cbStatus = CallbackStatus.未回收;

        FS.HISFC.BizLogic.HealthRecord.CaseHistory.CallBack cbMgr = new FS.HISFC.BizLogic.HealthRecord.CaseHistory.CallBack();
        FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();

        /// <summary>
        /// 设置查询类型
        /// </summary>
        [Category("病案查询状态"), Description("设置当前查询病案回收类型")]
        public CallbackStatus CbStatus
        {
            get { return cbStatus; }
            set { cbStatus = value; }
        }

        DateTime dtBeginUse = new DateTime(2011, 1, 1);
        /// <summary>
        /// 回收功能开始使用时间
        /// </summary>
        [Category("设置开始使用时间开关"), Description("开始使用的时间，限制未回收查询开始日期")]
        public DateTime DtBeginUse
        {
            get { return this.dtBeginUse; }
            set { this.dtBeginUse = value; }
        }
        /// <summary>
        /// 回收状态
        /// </summary>
        public enum CallbackStatus
        {
            /// <summary>
            /// 未回收
            /// </summary>
            未回收 = 0,
            /// <summary>
            /// 已回收
            /// </summary>
            已回收
        };

        private void neuBtnQuery_Click(object sender, EventArgs e)
        {
            //modify 2011-3-16 ch 时间格式化，保证是一天区间段内；
            this.OnQuery(this.neuDTBegin.Value.Date.AddHours(00).AddMinutes(00).AddSeconds(00), this.neuDTEnd.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59));
        }

        private void OnQuery(DateTime dtBegin, DateTime dtEnd)
        {
            if (ds != null)
            {
                ds.Clear();
            }

            string status = ((int)cbStatus).ToString();

            //sql = string.Format(sql, status, dtBegin, dtEnd, ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID);
            System.Collections.ArrayList al;
            string deptCode = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
            FS.HISFC.Models.Base.Department dept = deptMgr.GetDeptmentById(deptCode);

            if (dept == null)
            {
                MessageBox.Show("根据科室编码获取科室实体出错！");
                return;
            }
            if (dept.DeptType.Name == "护士站")
            {
                al = deptMgr.GetDeptFromNurseStation(dept);
                if (al == null || al.Count == 0)
                {
                    MessageBox.Show("查找病区对应的科室出错 null值");
                    return;
                }
                dept = (FS.HISFC.Models.Base.Department)al[0];
            }
            string sql = string.Empty;
            if (this.cbStatus == CallbackStatus.未回收)
            {
                if (dtBegin.Date < this.dtBeginUse)
                {
                    if (MessageBox.Show("查询开始日期应该大于等于系统模块使用日期" + this.dtBeginUse.ToShortDateString() + "，否则查询结果将都为未回收状态！是否继续查询", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }
                if (this.cbMgr.Sql.GetSql("Case.Callback.QueryDataSet.UnCallBack", ref sql) < 0)
                {
                    return;
                }
            }
            else if (this.cbStatus == CallbackStatus.已回收)
            {
                if (this.cbMgr.Sql.GetSql("Case.Callback.QueryDataSet.CallBack", ref sql) < 0)
                {
                    return;
                }
            }
            string docCode = string.Empty;
            if (this.checkBox1.Checked)
            {
                docCode = "ALL";
            }
            else
            {
                docCode = this.cbMgr.Operator.ID;
            }
            this.cbMgr.ExecQuery(string.Format(sql, status, dtBegin, dtEnd, dept.ID,docCode), ref ds);

            this.neuSpread1_Sheet1.DataSource = ds;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.PrintPage(this.neuPanel3.Left, this.neuPanel3.Top, this.neuPanel3);
            return base.OnPrint(sender, neuObject);
        }
    }
}
