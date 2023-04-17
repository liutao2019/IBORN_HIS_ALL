using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.OutpatientFee.FuYou.Report
{
    public partial class ucOutPatientIncomingMoneyComparison : FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public ucOutPatientIncomingMoneyComparison()
        {
            InitializeComponent();
        }

        #region  变量
        private bool isAll = false;
        /// <summary>
        /// 值为True时为查询全部科室，为false时为按登录科室查询
        /// </summary>
        [Category("设置"), Description("是否查询全部")]
        public bool IsAll
        {
            get
            {
                return isAll;
            }
            set { isAll = value; }
        }

        /// <summary>
        /// 有权限的职级编码
        /// </summary>
        private string lvlCode = "";

        /// <summary>
        /// 有权限的职级编码
        /// </summary>
        [Category("设置"), Description("有权限的职级编码")]
        public string LvlCode
        {
            get
            {
                return lvlCode;
            }
            set
            {
                lvlCode = value;
            }
        }
        #endregion

        FS.FrameWork.Management.DataBaseManger db = new FS.FrameWork.Management.DataBaseManger();
       
        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                MessageBox.Show("输入查询时间有误！");
                return -1;
            }
            FS.HISFC.BizLogic.Manager.Department de = new FS.HISFC.BizLogic.Manager.Department();
            string operName = ((FS.HISFC.Models.Base.Employee)de.Operator).Dept.Name;
            FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            FS.HISFC.Models.Base.Employee oper = interMgr.GetEmployeeInfo(de.Operator.ID);

            if (IsAll)
            {
                operName = "All";
            }
            else
            {
                if (!lvlCode.Contains(oper.Duty.ID))
                {
                    operName = "";
                }
            }
            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, operName);
        }

        private void ucOutPatientIncomingMoneyComparison_Load(object sender, EventArgs e)
        {
            DateTime nowTime = this.db.GetDateTimeFromSysDateTime();

            this.dtpEndTime.Value = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 23, 59, 59);
            this.dtpBeginTime.Value = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 00, 00, 01);
        }
    }
}
