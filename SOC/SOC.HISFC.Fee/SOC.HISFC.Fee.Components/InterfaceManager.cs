using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;

namespace FS.SOC.HISFC.Fee.Components
{
    /// <summary>
    /// [功能描述: 接口管理类]<br></br>
    /// [创 建 者: zhaoj]<br></br>
    /// [创建时间: 2011-11]<br></br>
    /// </summary>
    public class InterfaceManager
    {
        /// <summary>
        /// 获取业务操作信息消息收发接口
        /// </summary>
        /// <returns></returns>
        public static object GetBizInfoSenderImplement()
        {
            return FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(InterfaceManager), typeof(FS.SOC.HISFC.BizProcess.MessagePatternInterface.ISender));
        }

        /// <summary>
        /// 获取保存接口，默认为 Interface.SaveItem
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<FS.SOC.HISFC.Fee.Models.Undrug> GetISaveItem()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<FS.SOC.HISFC.Fee.Models.Undrug>>(typeof(FS.SOC.HISFC.Fee.Components.InterfaceManager), new Interface.SaveItem());
        }

        /// <summary>
        /// 获取保存接口，默认为 Interface.SaveItem
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<ArrayList> GetISaveAllItem()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<ArrayList>>(typeof(FS.SOC.HISFC.Fee.Components.InterfaceManager), new Interface.SaveAllItem());
        }

        /// <summary>
        /// 获取保存接口，默认为 Interface.SaveItem
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<ArrayList> GetISaveItemGroupDetail()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<ArrayList>>(typeof(FS.SOC.HISFC.Fee.Components.InterfaceManager), new Interface.SaveItemGroupDetail());
        }

        /// <summary>
        /// 获取项目组套查询接口
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.Fee.Interface.Components.IItemGroupQuery GetIItemGroupQuery()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.Fee.Interface.Components.IItemGroupQuery>(typeof(InterfaceManager), new Maintenance.ItemGroup.ucItemGroup());
        }

        /// <summary>
        /// 获取项目组套明细接口
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.Fee.Interface.Components.IItemGroupDetail GetIItemGroupDetail()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.Fee.Interface.Components.IItemGroupDetail>(typeof(InterfaceManager), new Maintenance.ItemGroup.ucItemGroupDetailNew());
        }

        /// <summary>
        /// 获取合同单位查询接口
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.Fee.Interface.Components.IPactInfoQuery GetIPactInfoQuery()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.Fee.Interface.Components.IPactInfoQuery>(typeof(InterfaceManager), new Maintenance.Pact.ucPactInfoManager());
        }

        /// <summary>
        /// 获取合同单位明细对照显示接口
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.Fee.Interface.Components.IPactFeeCodeRateDetail GetIPactFeeCodeRateDetail()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.Fee.Interface.Components.IPactFeeCodeRateDetail>(typeof(InterfaceManager), new Maintenance.Pact.ucFeeCodeRateDetail());
        }

        /// <summary>
        /// 获取合同单位维护的属性接口
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.Fee.Interface.Components.IPactInfoProperty GetIPactInfoProperty()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.Fee.Interface.Components.IPactInfoProperty>(typeof(InterfaceManager), new Maintenance.Pact.ucPropertyGrid());
        }

    }
}
