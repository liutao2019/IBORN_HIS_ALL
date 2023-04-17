using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.Local.RADT.GuangZhou.GYSY.Interface
{
    /// <summary>
    /// 修改过患者登记信息
    /// [创 建 者: kuangyh]<br></br>
    /// [创建时间: 2012-8]<br></br>
    /// </summary>
    public class ModifyPatient
    {
        /// <summary>
        /// 修改患者登记接口实现
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.RADT.Interface.Register.IInpatient GetModifyIInpatient()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.RADT.Interface.Register.IInpatient>(typeof(ModifyPatient), new FS.SOC.Local.RADT.GuangZhou.GYSY.Base.Inpatient.ucModifyInfo());
        }
    }
}
