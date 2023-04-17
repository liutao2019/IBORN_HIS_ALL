﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.Models.Fee.Item
{
    /// <summary>
    /// 终端确认枚举（门诊，住院，其他，全部）
    /// </summary>
    public enum EnumNeedConfirm
    {
        [FS.FrameWork.Public.Description("无")]
        None = 0,
        [FS.FrameWork.Public.Description("全部")]
        All=1,
        [FS.FrameWork.Public.Description("门诊")]
        Outpatient=2,
        [FS.FrameWork.Public.Description("住院")]
        Inpatient = 3
    }
}
