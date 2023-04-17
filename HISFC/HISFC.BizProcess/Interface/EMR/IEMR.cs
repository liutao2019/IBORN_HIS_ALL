using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.EMR
{
    /// <summary>
    /// 传染病报告接口类
    /// </summary>
    public interface IEMR
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        string ErrMsg
        {
            get;
        }

        /// <summary>
        /// 电子病历对外接口
        /// </summary>
        /// <param name="patient">患者信息</param>
        /// <param name="patientType">患者类型：0门诊或者1住院</param>
        /// <returns></returns>
        int EMRRegister(System.Windows.Forms.IWin32Window owner, FS.HISFC.Models.RADT.Patient patient, FS.HISFC.Models.Base.ServiceTypes patientType);
    }
}
