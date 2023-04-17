using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizProcess.Integrate
{
    /// <summary>
    /// [功能描述: 药品接口管理类]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-04]<br></br>
    /// </summary>
    public class PharmacyInterfaceManager
    {
       
        /// <summary>
        /// 获取摆药单本地化算法接口实现
        /// </summary>
        /// <returns></returns>
        public static object GetDrugBillClassImplement()
        {
            return FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.BizProcess.Integrate.Pharmacy), typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IDrugBillClass));
        }
    }
}
