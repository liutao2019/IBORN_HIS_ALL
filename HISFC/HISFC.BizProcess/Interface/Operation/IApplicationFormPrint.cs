using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.Operation
{
    /// <summary>
    /// [功能描述: 手术申请单打印接口]<br></br>
    /// [创 建 者: 王铁全]<br></br>
    /// [创建时间: 2007-01-04]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public interface IApplicationFormPrint : FS.FrameWork.WinForms.Forms.IReportPrinter
    {
        /// <summary>
        /// 手术申请单对象
        /// </summary>
        FS.HISFC.Models.Operation.OperationAppllication OperationApplicationForm
        {
            set;
        }

    }
}
