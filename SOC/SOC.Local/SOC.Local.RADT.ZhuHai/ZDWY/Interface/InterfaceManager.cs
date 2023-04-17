using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.Local.RADT.ZhuHai.ZDWY.Interface
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
        public static FS.SOC.HISFC.RADT.Interface.Register.IPatientList GetIPatientList()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.RADT.Interface.Register.IPatientList>(typeof(InterfaceManager), new FS.SOC.Local.RADT.ZhuHai.ZDWY.Base.Inpatient.ucPatientList());
        }

        /// <summary>
        /// 获取患者登记接口实现
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.RADT.Interface.Register.IInpatient GetIInpatient()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.RADT.Interface.Register.IInpatient>(typeof(InterfaceManager), new FS.SOC.Local.RADT.ZhuHai.ZDWY.Base.Inpatient.ucRegisterInfo());
        }

        /// <summary>
        /// 修改患者登记接口实现
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.RADT.Interface.Register.IInpatient GetModifyIInpatient()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.RADT.Interface.Register.IInpatient>(typeof(InterfaceManager), new FS.SOC.Local.RADT.ZhuHai.ZDWY.Base.Inpatient.ucModifyInfo());
        }

        /// <summary>
        /// 获取查询患者信息接口实现
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.RADT.Interface.Patient.IQuery GetIInpatientQuery()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.RADT.Interface.Patient.IQuery>(typeof(InterfaceManager), new FS.SOC.Local.RADT.ZhuHai.ZDWY.Base.Inpatient.frmQueryPatientInfoNew());
        }

        /// <summary>
        /// 获取保存后调用接口
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<FS.HISFC.Models.RADT.PatientInfo> GetPatientInfoISave()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<FS.HISFC.Models.RADT.PatientInfo>>(typeof(InterfaceManager), new SavePatient());
        }

        /// <summary>
        /// 获取ADT接口
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.BizProcess.MessagePatternInterface.IADT GetIADT()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.BizProcess.MessagePatternInterface.IADT>(typeof(InterfaceManager), null);
        }

        private static FS.HISFC.BizProcess.Interface.Account.IOperCard IOperCard = null;
        /// <summary>
        /// 读卡接口
        /// </summary>
        /// <returns></returns>
        public static FS.HISFC.BizProcess.Interface.Account.IOperCard GetIOperCard()
        {
            if (IOperCard == null)
            {
                IOperCard = FS.SOC.HISFC.BizProcess.CommonInterface.ControllerFactroy.Instance.CreateInferface<FS.HISFC.BizProcess.Interface.Account.IOperCard>(typeof(InterfaceManager), null);
            }
            return IOperCard;
        }

        /// <summary>
        /// 获取IReadIDCard接口
        /// </summary>
        /// <returns></returns>
        public static FS.HISFC.BizProcess.Interface.Account.IReadIDCard GetIReadIDCard()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.HISFC.BizProcess.Interface.Account.IReadIDCard>(typeof(InterfaceManager), null);
        }
    }
}
