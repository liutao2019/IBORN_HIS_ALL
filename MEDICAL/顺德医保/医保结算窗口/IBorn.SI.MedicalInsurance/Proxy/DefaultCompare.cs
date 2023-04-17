using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBorn.SI.MedicalInsurance.FoShan.Proxy
{
    /// <summary>
    /// 默认医保对照接口
    /// </summary>
    class DefaultCompare : IBorn.SI.BI.ICompare
    {


        #region ICompare 成员

        string IBorn.SI.BI.ICompare.ErrorMsg
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
