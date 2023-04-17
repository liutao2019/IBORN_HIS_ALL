using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Common
{
    /// <summary>
    /// [功能描述: 获取门诊药房接口的实现]<br></br>
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
        /// 获取门诊药房患者信息显示接口实现
        /// </summary>
        public static object GetOutpatientInfoLocalComponent()
        {
            return FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Common.InterfaceManager), typeof(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientShow));
        }

        /// <summary>
        /// 获取处方查询的发票、病例等编号的转换类
        /// </summary>
        /// <returns></returns>
        public static object GetRecipeQueryConvert()
        {
            return FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Common.InterfaceManager), typeof(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IRecipeQueryConvert));
        }

        /// <summary>
        /// 获取门诊药房打印接口实现
        /// </summary>
        /// <returns></returns>
        public static object GetOutpatientPrint()
        {
            return FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Common.InterfaceManager), typeof(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientDrug));
        }

        /// <summary>
        /// 获取门诊药房工作量接口实现
        /// </summary>
        /// <returns></returns>
        public static object GetOutpatientWorkLoad()
        {
            return FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Common.InterfaceManager), typeof(SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Local.Interface.IOutpatientWorkLoadBatch));
        }

        /// <summary>
        /// 获取门诊药房LED大屏显示接口实现
        /// </summary>
        /// <returns></returns>
        public static object GetOutpatientLED()
        {
            return FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Common.InterfaceManager), typeof(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientLED));
        }

        /// <summary>
        /// 获取业务扩展接口实现
        /// </summary>
        /// <returns></returns>
        public static object GetExtendBizImplement()
        {
            return FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Common.InterfaceManager), typeof(FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBizExtend));
        }

    }
}
