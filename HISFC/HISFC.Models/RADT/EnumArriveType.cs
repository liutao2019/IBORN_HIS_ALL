﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.Models.RADT
{
    /// <summary>
    /// 接诊类型
    /// </summary>
    public enum EnumArriveType
    {
        /// <summary>
        /// 登记
        /// </summary>
        [FS.FrameWork.Public.Description("接诊")]
        Accepts,

        /// <summary>
        /// 转入
        /// </summary>
        [FS.FrameWork.Public.Description("转入")]
        ShiftIn,

        /// <summary>
        /// 召回
        /// </summary>
        [FS.FrameWork.Public.Description("召回")]
        CallBack,

        /// <summary>
        /// 更换医师等信息
        /// </summary>
        [FS.FrameWork.Public.Description("换医师")]
        ChangeDoct
    }
}
