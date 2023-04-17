using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Neusoft.SOC.HISFC.BizProcess.CommonInterface;

namespace Neusoft.SOC.Local.RADT.GuangZhou.GYZL.Interface
{
    /// <summary>
    /// [功能描述: RADT接口管理类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public class InterfaceManager
    {
        /// <summary>
        /// 获取患者列表接口实现
        /// </summary>
        /// <returns></returns>
        public static Neusoft.SOC.HISFC.RADT.Interface.Register.IPatientList GetIPatientList()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<Neusoft.SOC.HISFC.RADT.Interface.Register.IPatientList>(typeof(InterfaceManager), new Neusoft.SOC.Local.RADT.GuangZhou.GYZL.Base.Inpatient.ucPatientList());
        }

        /// <summary>
        /// 获取患者登记接口实现
        /// </summary>
        /// <returns></returns>
        public static Neusoft.SOC.HISFC.RADT.Interface.Register.IInpatient GetIInpatient()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<Neusoft.SOC.HISFC.RADT.Interface.Register.IInpatient>(typeof(InterfaceManager), new Neusoft.SOC.Local.RADT.GuangZhou.GYZL.Base.Inpatient.ucRegisterInfo());
        }

        /// <summary>
        /// 修改患者登记接口实现
        /// </summary>
        /// <returns></returns>
        public static Neusoft.SOC.HISFC.RADT.Interface.Register.IInpatient GetModifyIInpatient()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<Neusoft.SOC.HISFC.RADT.Interface.Register.IInpatient>(typeof(InterfaceManager), new Neusoft.SOC.Local.RADT.GuangZhou.GYZL.Base.Inpatient.ucModifyInfo());
        }

        /// <summary>
        /// 获取查询患者信息接口实现
        /// </summary>
        /// <returns></returns>
        public static Neusoft.SOC.HISFC.RADT.Interface.Patient.IQuery GetIInpatientQuery()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<Neusoft.SOC.HISFC.RADT.Interface.Patient.IQuery>(typeof(InterfaceManager), new Neusoft.SOC.Local.RADT.GuangZhou.GYZL.Base.Inpatient.frmQueryPatientInfo());
        }

        /// <summary>
        /// 获取保存后调用接口
        /// </summary>
        /// <returns></returns>
        public static Neusoft.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<Neusoft.HISFC.Models.RADT.PatientInfo> GetPatientInfoISave()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<Neusoft.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<Neusoft.HISFC.Models.RADT.PatientInfo>>(typeof(InterfaceManager), new SavePatient());
        }

        /// <summary>
        /// 获取ADT接口
        /// </summary>
        /// <returns></returns>
        public static Neusoft.SOC.HISFC.BizProcess.MessagePatternInterface.IADT GetIADT()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<Neusoft.SOC.HISFC.BizProcess.MessagePatternInterface.IADT>(typeof(InterfaceManager), null);
        }

    }
}
