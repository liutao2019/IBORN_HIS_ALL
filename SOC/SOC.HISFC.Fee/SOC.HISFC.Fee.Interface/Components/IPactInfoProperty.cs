using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Fee.Interface.Components
{
    /// <summary>
    /// [功能描述: 合同单位属性]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// 说明：
    /// </summary>
    public interface IPactInfoProperty
    {
        void ShowProperty(params object[] selectObjects);

        void ShowDetailProperty(params object[] selectObjects);

        FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<Object[]> PropertyValueChanged
        {
            get;
            set;
        }
    }
}
