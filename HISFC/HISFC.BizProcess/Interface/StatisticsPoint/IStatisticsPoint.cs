using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.StatisticsPoint
{
    /// <summary>
    /// 数据统计节点通用接口
    /// {7930AB5C-6E33-4855-87E8-B87749639B88}
    /// 创建者：胡云贵
    /// 创建时间2020年7月13日
    /// </summary>
    public interface IStatisticsPoint
    {

        /// <summary>
        /// 数据统计项目
        /// </summary>
        /// <returns></returns>
        int SetValue(Object o);

        /// <summary>
        /// 设置患者信息
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        int SetPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo);

        /// <summary>
        /// 数据统计
        /// </summary>
        /// <returns></returns>
        int DoStatistics();

        /// <summary>
        /// 后期操作
        /// </summary>
        /// <returns></returns>
        int DoAfter();
    }
}
