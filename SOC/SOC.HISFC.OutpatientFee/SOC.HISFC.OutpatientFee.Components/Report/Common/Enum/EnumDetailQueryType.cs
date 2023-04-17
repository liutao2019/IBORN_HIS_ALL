using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Enum
{
    public enum EnumDetailQueryType
    {
        [FS.FrameWork.Public.Description("鼠标左键")]
        MouseClick,
        [FS.FrameWork.Public.Description("鼠标左键双击")]
        MouseDoubleClick,
        [FS.FrameWork.Public.Description("鼠标右键查看")]
        MouseRightSelect
    }
}
