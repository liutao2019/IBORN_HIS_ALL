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
using FS.SOC.Local.Registration.ShenZhen.Common;

namespace FS.SOC.Local.Registration.ShenZhen.BinHai.RegDayBalance
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
            DateTime beginFeeDate = DateTime.MinValue;
            string balanceNO="";
            int i = Function.GetDayReportBizProcess().GetRegisterLastBalanceDateAndNO(FS.FrameWork.Management.Connection.Operator.ID, ref beginTime, ref balanceNO);
            if (i < 0)
            {
                CommonController.CreateInstance().MessageBox("获取日结时间失败，原因：" + Function.GetDayReportBizProcess().Err, MessageBoxIcon.Error);
                return -1;
            }
            else
            {
                int j = Function.GetDayReportBizProcess().GetRegisterFeeDate(FS.FrameWork.Management.Connection.Operator.ID, ref beginFeeDate);

                if (beginTime < new DateTime(2010, 1, 1))
                {
                     beginTime = new DateTime(2010, 1, 1);
                    
                }
                if (beginTime < beginFeeDate)
                {
                    
                    System.TimeSpan interval = new TimeSpan();
                    // 作过日结
                    DateTime serverDate = CommonController.CreateInstance().GetSystemTime();

                    if (Convert.ToDateTime(beginTime) == System.DateTime.MinValue) return -1;

                    interval = serverDate.Subtract(Convert.ToDateTime(Convert.ToDateTime(beginFeeDate).ToString("D") + " 00:00:00"));

                    if (interval.Days > 0) //大于一天时:分开日结
                    {
                        MessageBox.Show("需要进行两次日结! 请先做前一天日结后,再做今天日结.");
                        this.dtEndTime.Value = Convert.ToDateTime(Convert.ToDateTime(beginFeeDate).ToString("D") + " 23:59:59");
                    }
                }

                else
                {
                    this.dtEndTime.Value = CommonController.CreateInstance().GetSystemTime();
                }

             
            }

            this.dtBeginTime.Value = beginTime.AddSeconds(1);

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

        public event ICommonReportController.FilterHandler OnFilterHandler;

        #endregion

        #region ITopQueryCondition 成员


        public object GetText(QueryControl common)
        {
            return this.GetValue(common);
        }

        #endregion
    }
}
