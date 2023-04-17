using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Fee.Report.Inpatient
{
    /// <summary>
    /// 住院科室实收收入月（日报表）
    /// </summary>
    public partial class ucInpatientDeptRealIncome : FS.SOC.Fee.Report.Base.ucReportBase
    {
        public ucInpatientDeptRealIncome()
        {
            InitializeComponent();
        }

        private FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
        FS.SOC.Fee.Report.InpatientReport.Base.InpatientFee inpatientReport = new FS.SOC.Fee.Report.InpatientReport.Base.InpatientFee();
        FS.HISFC.BizLogic.Fee.FeeCodeStat feeCodeStat = new FS.HISFC.BizLogic.Fee.FeeCodeStat();
        DateTime dtBegin = DateTime.MinValue;
        DateTime dtEnd = DateTime.MinValue;

        List<FS.HISFC.Models.Fee.FeeCodeStat> lstFeeStat = null;
        /// <summary>
        /// 报表类型
        /// </summary>
        string reportCode = "ZYTJ";

        protected override void Init()
        {
            dtEnd = conMgr.GetDateTimeFromSysDateTime();

            lstFeeStat = feeCodeStat.QueryFeeStatNameByReportCode(reportCode);

        }

        protected override DataTable Query(string strSQL, object[] param)
        {
            DataTable dtTable = null;
            switch (strSQL)
            {
                case "1":
                    dtTable = QueryInpatientDeptRealIncome();
                    break;

                case "2":
                    dtTable = QueryInpatientRealIncomeByDoctor();
                    break;
            }

            return dtTable;
        }


        #region 报表查询
        /// <summary>
        /// 住院收入报表 -- 按患者所在科室
        /// </summary>
        protected DataTable QueryInpatientDeptRealIncome()
        {
            DataTable dtReport = null;
            int iReturn = 0;
            //先查询所有科室收入
            string strStartTime = base.GetStartTimeParms();
            string strEndTime = base.GetEndTimeParms();
            string strDept = base.GetDeptParms();
            string strEmp = base.GetEmployeeParms();

            iReturn = inpatientReport.QueryInpatientIncomeByDept(reportCode, strDept, strEmp, FS.FrameWork.Function.NConvert.ToDateTime(strStartTime), FS.FrameWork.Function.NConvert.ToDateTime(strEndTime), lstFeeStat, out dtReport);
            
            return dtReport;
        }
        /// <summary>
        /// 住院收入报表 -- 按住院医生
        /// </summary>
        private DataTable QueryInpatientRealIncomeByDoctor()
        {
            DataTable dtReport = null;
            int iReturn = 0;

            string strStartTime = base.GetStartTimeParms();
            string strEndTime = base.GetEndTimeParms();
            string strDept = base.GetDeptParms();
            string strEmp = base.GetEmployeeParms();

            //先查询所有科室收入
            iReturn = inpatientReport.QueryInpatientIncomeByInhosDoctor(reportCode, strDept, strEmp, FS.FrameWork.Function.NConvert.ToDateTime(strStartTime), FS.FrameWork.Function.NConvert.ToDateTime(strEndTime), lstFeeStat, out dtReport);
            return dtReport;

        }
        #endregion

    }
}
