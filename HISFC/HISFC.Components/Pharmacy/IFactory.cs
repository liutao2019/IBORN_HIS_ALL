using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Components.Pharmacy
{
    /// <summary>
    /// [功能描述: 入出库业务工厂接口]<br></br>
    /// [创 建 者: 梁俊泽]<br></br>
    /// [创建时间: 2007-09]<br></br>
    /// </summary>
    public interface IFactory
    {
        /// <summary>
        /// 获取入库接口类
        /// </summary>
        /// <param name="inPrivType"></param>
        /// <param name="ucPhaManager"></param>
        /// <returns></returns>
        FS.HISFC.Components.Pharmacy.In.IPhaInManager GetInInstance(FS.FrameWork.Models.NeuObject inPrivType, FS.HISFC.Components.Pharmacy.In.ucPhaIn ucPhaManager);

        /// <summary>
        /// 获取出库接口类
        /// </summary>
        /// <param name="outPrivType"></param>
        /// <param name="ucPhaManager"></param>
        /// <returns></returns>
        FS.HISFC.Components.Pharmacy.In.IPhaInManager GetOutInstance(FS.FrameWork.Models.NeuObject outPrivType, FS.HISFC.Components.Pharmacy.Out.ucPhaOut ucPhaManager);
    }
}
