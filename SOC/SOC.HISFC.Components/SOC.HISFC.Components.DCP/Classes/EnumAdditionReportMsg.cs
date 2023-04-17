using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.HISFC.Components.DCP.Classes
{
    /// <summary>
    /// [功能描述： 传染病f附卡提示常数枚举]
    /// [创 建 者 ： 赵景]
    /// [创建时间： 2008-09]
    /// <修改记录>
    /// </修改记录>
    /// </summary>
    public enum EnumAdditionReportMsg
    {
        /// <summary>
        /// 需性病附卡
        /// </summary>
        NeedSexReport,
        /// <summary>
        /// 需附卡
        /// </summary>
        NeedAdditionReport,
        /// <summary>
        /// 需电话通知
        /// </summary>
        NeedPhoneNotice,
        /// <summary>
        /// 需填写结核病转诊单
        /// </summary>
        NeedWriteBill
    }
}
