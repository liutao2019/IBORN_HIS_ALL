using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.RADT;

namespace FS.SOC.HISFC.BizProcess.NurseInterface
{
    /// <summary>
    /// 住院护士站接诊、转入、召回接口
    /// </summary>
    public interface IArriveBase
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        string ErrInfo
        {
            get;
        }

        /// <summary>
        /// 住院护士站接诊、转入、召回操作
        /// </summary>
        /// <param name="arriveType">接诊类别</param>
        /// <param name="oldPatientInfo">旧的患者信息</param>
        /// <param name="newPatientInfo">新的患者信息</param>
        /// <returns></returns>
        int PatientArrive(FS.HISFC.Models.RADT.EnumArriveType arriveType, FS.HISFC.Models.RADT.PatientInfo oldPatientInfo, FS.HISFC.Models.RADT.PatientInfo newPatientInfo);
    }
}
