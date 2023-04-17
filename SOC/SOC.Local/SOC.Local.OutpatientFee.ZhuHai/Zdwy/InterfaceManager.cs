using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.Local.OutpatientFee.ZhuHai.Zdwy
{
    public class InterfaceManager
    {
        /// <summary>
        /// 获取执行科室的接口实现
        /// </summary>
        /// <returns></returns>
        public static FS.HISFC.BizProcess.Interface.Fee.IExecDept GetIExecDept()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.HISFC.BizProcess.Interface.Fee.IExecDept>(typeof(InterfaceManager), null);
        }
    }
}
