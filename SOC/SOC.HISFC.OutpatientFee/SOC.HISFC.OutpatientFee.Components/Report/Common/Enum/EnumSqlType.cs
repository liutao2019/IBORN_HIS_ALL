using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Enum
{
    public enum EnumSqlType
    {
        /// <summary>
        /// 主报表使用
        /// </summary>
        [FS.FrameWork.Public.Description("主报表使用")]
        MainReportUsing,
        
        /// <summary>
        /// 明细报表使用
        /// </summary>
        [FS.FrameWork.Public.Description("明细报表使用")]
        DetailReportUsing,

        /// <summary>
        /// 条件使用
        /// </summary>
        [FS.FrameWork.Public.Description("条件使用")]
        ConditionUsing,

        /// <summary>
        /// 报表分组使用
        /// </summary>
        [FS.FrameWork.Public.Description("报表分组使用")]
        TableGroupUsing
    }
}
