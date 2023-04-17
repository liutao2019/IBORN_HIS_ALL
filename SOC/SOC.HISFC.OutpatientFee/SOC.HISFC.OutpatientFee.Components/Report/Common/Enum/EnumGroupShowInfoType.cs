using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Enum
{
    /// <summary>
    /// 分组显示信息类型
    /// </summary>
    public enum EnumGroupShowInfoType
    {
        /// <summary>
        /// 自定义
        /// </summary>
        [FS.FrameWork.Public.Description("自定义")]
        Custom,
        /// <summary>
        /// 分组内容
        /// </summary>
        [FS.FrameWork.Public.Description("分组内容")]
        GroupColumn,
        /// <summary>
        /// 分组内容+自定义内容
        /// </summary>
        [FS.FrameWork.Public.Description("分组内容+自定义内容")]
        GroupColumnAndCustom,
        /// <summary>
        /// 自定义内容+分组内容
        /// </summary>
        [FS.FrameWork.Public.Description("自定义内容+分组内容")]
        CustomAndGroupColumn
    }
}
