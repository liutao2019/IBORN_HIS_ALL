using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Neusoft.SOC.HISFC.BizProcess.CommonInterface;

namespace Neusoft.SOC.HISFC.RADT.Interface.Patient
{
    /// <summary>
    /// [功能描述: 患者基本信息接口]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public interface IPatient : ILifecycle, ILoadable, IClearable,IValidabe
    {
        /// <summary>
        /// 从界面获取患者信息
        /// </summary>
        /// <returns></returns>
        Neusoft.HISFC.Models.RADT.Patient GetPatient();

        /// <summary>
        /// 将患者信息显示到界面
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int ShowPatient(Neusoft.HISFC.Models.RADT.Patient patient);

    }
}
