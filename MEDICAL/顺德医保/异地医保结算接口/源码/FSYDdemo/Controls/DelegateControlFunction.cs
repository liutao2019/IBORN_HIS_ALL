using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FoShanYDSI.Controls
{
    /// <summary>
    /// 入院登记接口
    /// </summary>
    /// <param name="patient"></param>
    /// <param name="personInfo"></param>
    /// <param name="status"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    public delegate int InPatientReg(FS.HISFC.Models.RADT.PatientInfo patient, FoShanYDSI.Objects.SIPersonInfo personInfo, ref string status, ref string msg);

    /// <summary>
    /// 住院结算
    /// </summary>
    /// <param name="patient"></param>
    /// <param name="personInfo"></param>
    /// <param name="feeDetails"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    public delegate int InPatientBalance(FS.HISFC.Models.RADT.PatientInfo patient, FoShanYDSI.Objects.SIPersonInfo personInfo, ref ArrayList feeDetails, ref string msg, ArrayList arrParm);

}
