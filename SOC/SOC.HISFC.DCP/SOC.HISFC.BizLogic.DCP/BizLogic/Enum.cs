using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.HISFC.DCP.Enum
{
    /// <summary>
    /// 患者类型
    /// </summary>
    public enum PatientType
    {
        /// <summary>
        /// 住院患者
        /// </summary>
        I,

        /// <summary>
        /// 门诊患者
        /// </summary>
        C,

        /// <summary>
        /// 其他
        /// </summary>
        O
    }

    /// <summary>
    /// 报告卡状态
    /// </summary>
    public enum ReportState
    {
        /// <summary>
        /// 新填
        /// </summary>
        New = 0,

        /// <summary>
        /// 合格
        /// </summary>
        Eligible = 1,

        /// <summary>
        /// 不合格
        /// </summary>
        UnEligible = 2,

        /// <summary>
        /// 报告人作废
        /// </summary>
        OwnCancel = 3,

        /// <summary>
        /// 保健科作废
        /// </summary>
        Cancel = 4
    }

    /// <summary>
    ///  报卡操作结果
    /// </summary>
    public enum ReportOperResult
    {
        /// <summary>
        /// 完成报卡
        /// </summary>
        OK,
        /// <summary>
        /// 取消了报卡
        /// </summary>
        Cancel,
        /// <summary>
        /// 其它[例如：调用CheckDisease时相应诊断不需要报卡]
        /// </summary>
        Other
    }
}
