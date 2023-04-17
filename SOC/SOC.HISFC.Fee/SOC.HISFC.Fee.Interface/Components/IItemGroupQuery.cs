using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.Fee.Interface.Components
{
    /// <summary>
    /// [功能描述: 物价组套基本信息查询界面接口]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// 说明：
    /// </summary>
    public interface IItemGroupQuery : IDataDetail,IDisposable
    {
        /// <summary>
        /// 项目选择变化
        /// </summary>
        FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Fee.Models.Undrug> SelectedItemGroupChange
        {
            get;
            set;
        }

        /// <summary>
        /// 增加组套项目
        /// </summary>
        /// <returns></returns>
        int AddGroupItem();

        /// <summary>
        /// 修改组套项目
        /// </summary>
        /// <returns></returns>
        int ModifyGroupItem();
    }
}
