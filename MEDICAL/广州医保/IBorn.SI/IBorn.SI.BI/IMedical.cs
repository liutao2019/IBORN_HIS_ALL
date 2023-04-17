using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBorn.SI.BI
{
    /// <summary>
    /// 医保接口
    /// </summary>
    public interface IMedical
    {
        IBalance CreateBalance();

        IRADT CreateRADT();

        ICompare CreateCompare();

        IUpload CreateUpload();
    }
}
