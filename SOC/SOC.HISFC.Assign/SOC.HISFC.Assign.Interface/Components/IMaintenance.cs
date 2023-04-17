using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;

namespace FS.SOC.HISFC.Assign.Interface.Components
{
    /// <summary>
    /// [功能描述: 基础信息维护接口]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public interface IMaintenance<T> : IInitialisable, IClearable, IDisposable, ILoadable
    {
        int Init(ArrayList al);

        int Add(T t);

        int Modify(T t);

        FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<T> SaveInfo
        {
            get;
            set;
        }
    }
}
