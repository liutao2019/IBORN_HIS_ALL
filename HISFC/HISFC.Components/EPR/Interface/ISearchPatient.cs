using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Components.EPR.Interface
{
    public delegate void ObjectHandle(FS.FrameWork.Models.NeuObject patient);
    /// <summary>
    /// 查找患者接口
    /// </summary>
    public interface ISearchPatient
    {
        event ObjectHandle OnSelectedPatient;
        System.Windows.Forms.Control SearchControl { get;}
    }
}
