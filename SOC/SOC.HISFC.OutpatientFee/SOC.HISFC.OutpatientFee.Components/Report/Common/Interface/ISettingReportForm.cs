using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Interface
{
    public interface ISettingReportForm
    {
        void SetValue(ReportQueryInfo value);

        ReportQueryInfo GetValue();
    }
}
