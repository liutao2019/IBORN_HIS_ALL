using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.WinForms.Controls;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Interface;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.ControlType;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.DayBalanceCheck
{
    public partial class ucTopQueryCondition : FS.FrameWork.WinForms.Controls.ucBaseControl, ITopQueryCondition
    {

        public ucTopQueryCondition()
        {
            InitializeComponent();
        }

        #region ITopQueryCondition 成员

        public int Init()
        {
            //起始时间
            DateTime beginTime = DateTime.MinValue;
            string balanceNO="";
            //{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
            FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            if (employee == null)
            {
                employee = new FS.HISFC.Models.Base.Employee();
            }
            FS.HISFC.Models.Base.Department dept = employee.Dept as FS.HISFC.Models.Base.Department;
            if (dept == null)
            {
                dept = new FS.HISFC.Models.Base.Department();
            }
            string hospitalid = dept.HospitalID;
            int i = Function.GetDayReportBizProcess().GetClinicFeeLastBalanceDateAndNO(FS.FrameWork.Management.Connection.Operator.ID,hospitalid, ref beginTime, ref balanceNO);
            if (i < 0)
            {
                CommonController.CreateInstance().MessageBox("获取日结时间失败，原因：" + Function.GetDayReportBizProcess().Err, MessageBoxIcon.Error);
                return -1;
            }
            if (beginTime < new DateTime(1753, 1, 1))
            {
                beginTime = new DateTime(1753, 1, 1);
            }

            this.dtBeginTime.Value = beginTime.AddSeconds(1);
            this.dtEndTime.Value = CommonController.CreateInstance().GetSystemTime();

            if (this.dtBeginTime.Value > this.dtEndTime.Value)
            {
                CommonController.CreateInstance().MessageBox("日结时间不能大于当前时间", MessageBoxIcon.Error);
                return -1;
            }

            return 1;
        }

        public void AddControls(List<QueryControl> list)
        {

        }

        public object GetValue(QueryControl common)
        {
            Control[] controls = this.Controls.Find(common.Name, true);
            if (controls != null && controls.Length > 0)
            {
                if (common.ControlType is DateTimeType)
                {
                    NeuDateTimePicker dt = controls[0] as NeuDateTimePicker;
                    return dt.Value;
                }
            }

            return "";
        }

        public object GetText(QueryControl common)
        {
            Control[] controls = this.Controls.Find(common.Name, true);
            if (controls != null && controls.Length > 0)
            {
                if (common.ControlType is DateTimeType)
                {
                    NeuDateTimePicker dt = controls[0] as NeuDateTimePicker;
                    return dt.Value;
                }
            }

            return "";
        }
        public event ICommonReportController.FilterHandler OnFilterHandler;

        #endregion
    }
}
