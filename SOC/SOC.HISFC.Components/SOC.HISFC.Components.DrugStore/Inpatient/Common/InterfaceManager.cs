using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Components.DrugStore.Inpatient.Common
{
     /// <summary>
    /// [功能描述: 获取住院药房接口的实现]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// 说明：
    /// 1、所有打印接口
    /// 2、所有外部程序接口
    /// 3、所有本地化接口
    /// </summary>
    public class InterfaceManager
    {
        /// <summary>
        /// 获取住院摆药用的打印接口
        /// </summary>
        /// <returns></returns>
        public static object GetInpatientDrug()
        {
            return FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.HISFC.Components.DrugStore.Inpatient.Common.InterfaceManager), typeof(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientDrug));
        }

        /// <summary>
        /// 获取住院摆药单维护的接口
        /// </summary>
        /// <returns></returns>
        public static object GetDrugBillClass()
        {
            return FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.HISFC.Components.DrugStore.Inpatient.Common.InterfaceManager), typeof(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IDrugBillClass));
        }

        /// <summary>
        /// 获取业务扩展接口实现
        /// </summary>
        /// <returns></returns>
        public static object GetExtendBizImplement()
        {
            return FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.HISFC.Components.DrugStore.Inpatient.Common.InterfaceManager), typeof(FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBizExtend));
        }

    }
}
