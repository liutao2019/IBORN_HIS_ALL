using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Components.Pharmacy
{
    /// <summary>
    /// [功能描述: 药库接口管理类]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// </summary>
    public class InterfaceManager
    {
        /// <summary>
        /// 获取数据选择列表的接口实现
        /// </summary>
        /// <returns></returns>
        public static object GetDataChooseListControl()
        {
            return FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.HISFC.Components.Pharmacy.InterfaceManager), typeof(FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataChooseList));
        }

        /// <summary>
        /// 获取明细数据显示接口实现
        /// </summary>
        /// <returns></returns>
        public static object GetDataDetailControl()
        {
            return FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.HISFC.Components.Pharmacy.InterfaceManager), typeof(SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataDetail));
        }
    
        /// <summary>
        /// 获取业务扩展接口实现
        /// </summary>
        /// <returns></returns>
        public static object GetExtendBizImplement()
        {
            return FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.HISFC.Components.Pharmacy.InterfaceManager), typeof(FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBizExtend));
        }

        /// <summary>
        /// 获取打印单据
        /// </summary>
        /// <returns></returns>
        public static object GetBillPrintImplement()
        {
            return FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.HISFC.Components.Pharmacy.InterfaceManager), typeof(FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBill));
        }

        /// <summary>
        /// 获取消息发送接口实现
        /// </summary>
        /// <returns></returns>
        public static object GetBizInfoSenderImplement()
        {
            return FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.HISFC.Components.Pharmacy.InterfaceManager), typeof(FS.SOC.HISFC.BizProcess.MessagePatternInterface.ISender));
        }

        /// <summary>
        /// 药库入库业务控件接口
        /// 注意使用了index区分入库和出库，这里定义1为入库
        /// </summary>
        /// <returns></returns>
        public static object GetPharmacyInputControl()
        {
            return FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.HISFC.Components.Pharmacy.InterfaceManager), typeof(FS.SOC.HISFC.Components.Pharmacy.Base.IPharmacyBizManager),1);
        }

        /// <summary>
        /// 药库出库业务控件接口
        /// 注意使用了index区分入库和出库，这里定义2为出库
        /// </summary>
        /// <returns></returns>
        public static object GetPharmacyOutputControl()
        {
            return FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.HISFC.Components.Pharmacy.InterfaceManager), typeof(FS.SOC.HISFC.Components.Pharmacy.Base.IPharmacyBizManager),2);
        }

        /// <summary>
        /// 金额显示接口
        /// </summary>
        /// <returns></returns>
        public static object GetCostShowImplement()
        {
            return FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.HISFC.Components.Pharmacy.InterfaceManager), typeof(FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ICostShow));
        }
    }
}
