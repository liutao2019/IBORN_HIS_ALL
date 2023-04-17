using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.DefultInterfacesAchieve.RADT
{
    /// <summary>
    /// 出院、出院召回等地方的判断,是否可以执行下一步操作
    /// </summary>
    public class PatientShiftValidAchieve : FS.HISFC.BizProcess.Interface.IPatientShiftValid
    {

        #region IPatientShiftValid 成员

        //FS.HISFC.BizProcess.Integrate.Manager constManager = new FS.HISFC.BizProcess.Integrate.Manager();

        //Function.InPatient judgeMgr = new FS.DefultInterfacesAchieve.Function.InPatient();

        //FS.HISFC.BizLogic.RADT.InPatient inPatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// 病人变化判断接口
        /// </summary>
        /// <param name="p">患者信息</param>
        /// <param name="type">变化类型</param>
        /// <param name="err">传出错误文本</param>
        /// <returns></returns>
        public bool IsPatientShiftValid(FS.HISFC.Models.RADT.PatientInfo p, FS.HISFC.Models.Base.EnumPatientShiftValid type, ref string err)
        {
            #region 出院登记判断

            if (type == FS.HISFC.Models.Base.EnumPatientShiftValid.O)
            {
            }

            #endregion

            #region 出院和转科

            if (type == FS.HISFC.Models.Base.EnumPatientShiftValid.O || type == FS.HISFC.Models.Base.EnumPatientShiftValid.R)
            {
            }
            #endregion

            return true;
        }

        #endregion
    }
}
