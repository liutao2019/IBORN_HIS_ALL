using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.Assign.Interface.Components;
using FS.SOC.HISFC.Assign.Components.Common.Display;

namespace FS.SOC.HISFC.Assign.Components.Interface
{
    /// <summary>
    /// [功能描述: 门诊分诊大屏默认显示]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public  class DefualtDisplay:IAssignDisplay
    {
        frmDefualtDisplay frm = null;

        #region IAssignDisplay 成员

        public void Show()
        {
            frm = new frmDefualtDisplay();
            frm.Show();
        }

        public void Close()
        {
            if (frm != null && !frm.IsDisposed)
            {
                frm.Close();
            }
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            if (frm != null && !frm.IsDisposed)
            {
                frm.Close();
                frm.Dispose();
            }
        }

        #endregion
    }
}
