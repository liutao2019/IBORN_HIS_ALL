using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.InpatientFee.Interface
{
    /// <summary>
    /// [功能描述: 自动收费接口，用于各种后台收费：规则收费、固定费用收费、辅材收费等]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public interface IAutoFee
    {
        /// <summary>
        /// 费用来源
        /// </summary>
        FS.HISFC.Models.Fee.Inpatient.FTSource FTSource
        {
            set;
        }

        /// <summary>
        /// 处理数据（收取多人）
        /// </summary>
        /// <param name="patientList"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        int Process(List<FS.HISFC.Models.RADT.PatientInfo> patientList, DateTime dtBegin, DateTime dtEnd,ref string errorInfo,params object[] appendParams);

        /// <summary>
        /// 处理数据（收取单人）
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="errorInfo"></param>
        /// <param name="appendParams"></param>
        /// <returns></returns>
        int Process(FS.HISFC.Models.RADT.PatientInfo patientInfo, DateTime dtBegin, DateTime dtEnd, ref string errorInfo, params object[] appendParams);

        /// <summary>
        /// 清空数据
        /// </summary>
        /// <returns></returns>
        void Clear();

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <returns></returns>
        int Init();
    }
}
