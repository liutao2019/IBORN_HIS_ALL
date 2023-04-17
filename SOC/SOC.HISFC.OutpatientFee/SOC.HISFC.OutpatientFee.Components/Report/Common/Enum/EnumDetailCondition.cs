using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Enum
{
    public enum EnumDetailCondition
    {
        [FS.FrameWork.Public.Description("使用主报表的条件")]
        UseMainCondition,
        [FS.FrameWork.Public.Description("使用主报表的数据")]
        UseMainData,
        [FS.FrameWork.Public.Description("自定义")]
        Custom
    }
}
