using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Enum
{
    public enum EnumDetailShowType
    {
        [FS.FrameWork.Public.Description("显示对话框")]
        ShowDialog,
        [FS.FrameWork.Public.Description("显示控件")]
        ShowControl,
        [FS.FrameWork.Public.Description("显示Tab页")]
        ShowTabControl,
        [FS.FrameWork.Public.Description("汇总明细左右显示")]
        LeftAndRight
    }
}
