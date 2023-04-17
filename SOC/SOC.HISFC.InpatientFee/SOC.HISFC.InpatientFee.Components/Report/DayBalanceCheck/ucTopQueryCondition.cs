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

namespace FS.SOC.HISFC.InpatientFee.Components.Report.DayBalanceCheck
{
    public partial class ucTopQueryCondition : FS.FrameWork.WinForms.Controls.ucBaseControl, ICommonReportController.ITopQueryCondition
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
            int i = Function.GetDayReportBizProcess().GetLastBalanceDateAndNO(FS.FrameWork.Management.Connection.Operator.ID, ref beginTime, ref balanceNO);
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

        public void AddControls(List<CommonReportQueryInfo> list)
        {

        }

        public object GetValue(CommonReportQueryInfo common)
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
