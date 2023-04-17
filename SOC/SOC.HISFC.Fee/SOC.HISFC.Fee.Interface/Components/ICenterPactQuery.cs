using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.Fee.Interface.Components
{
    public interface ICenterPactQuery : IDataDetail, IDisposable
    {
        /// <summary>
        /// 医保类型选择变化
        /// </summary>
        FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<string> SelectedCenterInfoChange
        {
            get;
            set;

            
        }
    }
}
