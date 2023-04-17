using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.OrderInterface.Common
{
    /// <summary>
    /// 医疗结果查询
    /// 这里是汇总查询，整合所有的医疗结果：检验、PACS、心电、超声、内镜、院感等
    /// </summary>
    public interface IMedicalResult
    {
        string ErrInfo
        {
            set;
            get;
        }

        /// <summary>
        /// 查询结果类型：检验、PACS、心电、超声、内镜、院感等
        /// </summary>
        EnumResultType ResultType
        {
            get;
            set;
        }

        /// <summary>
        /// 查询结果
        /// 医嘱不为空，则显示单个医嘱的结果；否则显示个人所有结果
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        int ShowResult(FS.HISFC.Models.RADT.Patient patient, ArrayList alOrder);
    }

    /// <summary>
    /// 医疗结果类别
    /// </summary>
    public enum EnumResultType
    {
        /// <summary>
        /// 检验系统 1
        /// </summary>
        LIS = 1,

        /// <summary>
        /// 检查系统 2
        /// </summary>
        PACS,

        /// <summary>
        /// 超声系统 3
        /// </summary>
        USIS,

        /// <summary>
        /// 心电系统 4
        /// </summary>
        ECG,

        /// <summary>
        /// 内镜系统 5
        /// </summary>
        EIS,

        /// <summary>
        /// 病理系统 6
        /// </summary>
        PIS,

        /// <summary>
        /// 院内感染 6
        /// </summary>
        NosocomialInfections
    }
}
