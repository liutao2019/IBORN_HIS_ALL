using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.Assign.Interface.Components
{
    /// <summary>
    /// [功能描述: 门诊分诊患者信息显示接口]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public interface IPatientInfo:IClearable,IDisposable
    {
        /// <summary>
        /// 显示患者信息
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        int ShowPatientInfo(FS.HISFC.Models.Registration.Register register);

        /// <summary>
        /// 显示已分诊患者信息
        /// </summary>
        /// <param name="assign"></param>
        /// <returns></returns>
        int ShowPatientInfo(FS.SOC.HISFC.Assign.Models.Assign assign);
    }
}
