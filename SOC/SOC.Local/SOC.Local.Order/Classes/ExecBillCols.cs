using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace FS.SOC.Local.Order.Classes
{
    /// <summary>
    /// 门诊主界面列
    /// </summary>
    [ComVisible(true)]
    public enum ExecBillCols
    {
        /// <summary>
        /// 医嘱时间
        /// </summary>
        OrderState = 0,
        /// <summary>
        /// 床号
        /// </summary>
        BedID,
        /// <summary>
        /// 患者姓名
        /// </summary>
        PatientName,
        /// <summary>
        /// 组合标记
        /// </summary>
        ComboNo,
        /// <summary>
        /// 组合标记
        /// </summary>
        ComboMemo,
        /// <summary>
        /// 医嘱名称
        /// </summary>
        ItemName,
        /// <summary>
        /// 医嘱数量
        /// </summary>
        OrderQty,
        /// <summary>
        /// 频次编码
        /// </summary>
        FrequencyID,
        /// <summary>
        /// 用法名称
        /// </summary>
        UsageName,
        /// <summary>
        /// 每次用量
        /// </summary>
        DoseOnce,
        /// <summary>
        /// 预计执行时间
        /// </summary>
        Memo,
        /// <summary>
        /// 执行签名
        /// </summary>
        ExecSignature,
        /// <summary>
        /// 执行时间
        /// </summary>
        ExecTime,
        /// <summary>
        /// 打印标记
        /// </summary>
        PrintFlag
    }
}
